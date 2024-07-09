using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class MapService : BaseService
{
    private readonly IRepository<Trip, int> _tripRepo;
    private readonly IRepository<TripShape, string> _tripShapeRepo;
    private readonly IRepository<Route, int> _routeRepo;
    private readonly ISeptaApiClient _septaApiClient;

    public MapService(IRepository<Trip, int> tripRepo, IRepository<TripShape, string> tripShapeRepo, IRepository<Route, int> routeRepo, ISeptaApiClient septaApiClient, IRepository<Stop, int> stopRepo, IRepository<StopTime, int> stopTimeRepo)
    {
        _tripRepo = tripRepo;
        _tripShapeRepo = tripShapeRepo;
        _routeRepo = routeRepo;
        _septaApiClient = septaApiClient;
    }
    
    public async Task<List<Shape>> GetShapesByRoute(int routeId)
    {
        Trip routeTrip = await _tripRepo.GetAll()
                             .FirstOrDefaultAsync(trip => trip.RouteId == routeId) 
                         ?? throw new InvalidOperationException($"No trips found for route ID {routeId}");

        List<Shape> shapes = await _tripShapeRepo.GetAll()
            .Where(ts => ts.TripId == routeTrip.Id)
            .Select(ts => ts.Shape)
            .ToListAsync();
        
        return shapes;
    }

    public Task<List<Stop>> GetStopsByAgencyAndRouteType(string agencyName, RouteType routeType)
    {
        List<Stop> routeStops = new List<Stop>();
        
        var routes = _routeRepo.GetAll()
            .Include(route => route.Trips)
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals(agencyName)
                            && route.Type == (int)routeType);

        foreach (var route in routes)
        {
            var trip = route.Trips.First();

            var stops = trip.StopTimes.Select(st => st.Stop).ToList();
            
            routeStops.AddRange(stops);
        }

        return Task.FromResult(routeStops);
    }

    public async Task<Dictionary<string, List<Shape>>> GetShapeData(RouteType routeType, string agencyName)
    {
        List<Route> routes = await _routeRepo.GetAll()
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals(agencyName) 
                            && route.Type == (int)routeType)
            .ToListAsync();

        var shapes = new Dictionary<string, List<Shape>>();

        foreach (var route in routes)
        {
            List<Shape> routeShapes = await GetShapesByRoute(route.Id);

            if (route.ShortName != null)
            {
                shapes.TryAdd(route.ShortName, routeShapes);
            }
        }
        
        return shapes;
    }
    
    public async Task<List<TrainView>> GetTrainView()
    {
        JToken response = await _septaApiClient.RegionalRailTrainView();
        
        var trainsOnSystem = new List<TrainView>();

        foreach (JToken trainData in response)
        {
            
            var train = new TrainView
            {
                Latitude = ParseFloatValue(trainData, "lat"),
                Longitude = ParseFloatValue(trainData, "lon"),
                TrainNumber = RemoveSpecialCharacters(ParseStringValue(trainData, "trainno")),
                ServiceType = ParseStringValue(trainData, "service"),
                Destination = ParseStringValue(trainData, "destination"),
                CurrentStop = ParseStringValue(trainData, "currentstop"),
                NextStop = ParseStringValue(trainData, "nextstop"),
                Line = ParseStringValue(trainData, "line"),
                Consist = ParseStringValue(trainData, "consist"),
                Heading = ParseFloatValue(trainData, "heading"),
                MinutesLate = ParseIntValue(trainData, "late"),
                Source = ParseStringValue(trainData, "SOURCE"),
                Track = ParseStringValue(trainData, "TRACK"),
                TrackChange = ParseStringValue(trainData, "TRACK_CHANGE")
            };
            trainsOnSystem.Add(train);
        }

        return trainsOnSystem;
    }
    
    public async Task<List<TransitView>> GetTransitView(string routeNumber)
    {
        JObject response = await _septaApiClient.TransitView(routeNumber);

        JProperty data = response.Properties().First();

        JToken transitView = data.Value;
        
        var vehiclesOnSystem = new List<TransitView>();

        foreach (var transitData in transitView)
        {
            var vehicle = new TransitView
            {
                Latitude = ParseFloatValue(transitData, "lat"),
                Longitude = ParseFloatValue(transitData, "lon"),
                Label = ParseStringValue(transitData, "label"),
                VehicleId = ParseStringValue(transitData, "VehicleID"),
                BlockId = ParseStringValue(transitData, "BlockID"),
                Direction = ParseStringValue(transitData, "direction"),
                Offset = ParseStringValue(transitData, "Offset"),
                Heading = ParseIntValue(transitData, "heading"),
                OriginalMinutesLate = ParseIntValue(transitData, "original_late"),
                Trip = ParseStringValue(transitData, "trip"),
                NextStopId = ParseStringValue(transitData, "next_stop_id"),
                NextStopName = ParseStringValue(transitData, "next_stop_name"),
                NextStopSequence = ParseIntValue(transitData, "next_stop_sequence"),
                SeatAvailability = ParseStringValue(transitData, "estimated_seat_availability"),
                TimeStamp = ParseIntValue(transitData, "timestamp")
            };
            
            vehiclesOnSystem.Add(vehicle);
        }

        return vehiclesOnSystem;
    }
    
}