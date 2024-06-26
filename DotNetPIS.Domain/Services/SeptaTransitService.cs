using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class SeptaTransitService : BaseService
{
    private readonly ISeptaApiClient _septaApiClient;
    private readonly IRepository<Route, int> _routeRepo;
    
    public SeptaTransitService(ISeptaApiClient septaApiClient, IRepository<Route, int> routeRepo)
    {
        _septaApiClient = septaApiClient;
        _routeRepo = routeRepo;
    }
    
    private async Task<List<TransitView>> GetTransitDataByType(RouteType routeType)
    {
        var vehicles = new List<TransitView>();
        
        List<Route> routes = await _routeRepo.GetAll()
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals("SEPTA") 
                            && route.Type == (int)routeType)
            .ToListAsync();

        foreach (var route in routes)
        {
            var routeData = await GetTransitView(route.RouteNumber);
            vehicles.AddRange(routeData);
        }
        
        return vehicles;
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