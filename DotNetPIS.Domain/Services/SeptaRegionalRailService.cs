using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class SeptaRegionalRailService : BaseJsonService
{
    private readonly ISeptaApiClient _septaApiClient;

    public SeptaRegionalRailService(ISeptaApiClient septaApiClient)
    {
        _septaApiClient = septaApiClient;
    }

    public async Task<List<Arrival>> GetRegionalRailArrivals(string stationName, string direction, int results = 10)
    {
        JObject response = await _septaApiClient.GetRegionalRailArrivals(stationName, direction, results);

        JProperty data = response.Properties().First();

        var stationArrivals = new List<Arrival>();

        JToken? arrivals;

        if (direction.Equals("N"))
        {
            arrivals = data.Value[0]!["Northbound"];
        }
        else
        {
            arrivals = data.Value[0]!["Southbound"];
        }

        foreach (var trainData in arrivals!)
        {
            var arrival = new Arrival
            {
                Direction = ParseStringValue(trainData, "direction"),
                Path = ParseStringValue(trainData, "path"),
                TrainId = RemoveSpecialCharacters(ParseStringValue(trainData, "train_id")),
                Origin = ParseStringValue(trainData, "origin"),
                Destination = ParseStringValue(trainData, "destination"),
                Line = ParseStringValue(trainData, "line"),
                Status = ParseStringValue(trainData, "status"),
                ServiceType = ParseStringValue(trainData, "service_type"),
                NextStation = ParseStringValue(trainData, "next_station"),
                SchedTime = ParseDateTimeString(trainData, "sched_time"),
                DepartTime = ParseDateTimeString(trainData, "depart_time"),
                Track = ParseStringValue(trainData, "track"),
                TrackChange = ParseStringValue(trainData, "track_change"),
                Platform = ParseStringValue(trainData, "platform"),
                PlatformChange = ParseStringValue(trainData, "platform_change")
            };
            stationArrivals.Add(arrival);
        }
        return stationArrivals;
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

    public async Task<List<NextToArrive>> GetNextToArrive(string startingStation, string endingStation, int results)
    {
        JObject response = await _septaApiClient.GetNextToArrive(startingStation, endingStation, results);

        JProperty data = response.Properties().First();

        JToken nextToArrive = data.Value;

        var nextTrainsToArrive = new List<NextToArrive>();

        foreach (var trainData in nextToArrive)
        {
            var train = new NextToArrive
            {
                OriginTrain = ParseStringValue(trainData, "orig_train"),
                OriginLine = ParseStringValue(trainData, "orig_line"),
                OriginDepartureTime = ParseStringValue(trainData, "orig_departure_time"),
                OriginArrivalTime = ParseStringValue(trainData, "orig_arrival_time"),
                OriginDelay = ParseStringValue(trainData, "orig_delay"),
                TerminatingTrain = ParseStringValue(trainData, "term_train"),
                TerminatingLine = ParseStringValue(trainData, "term_line"),
                TerminatingDepartureTime = ParseStringValue(trainData, "term_depart_time"),
                TerminatingArrivalTime = ParseStringValue(trainData, "term_arrival_time"),
                Connection = ParseStringValue(trainData, "Connection"),
                TerminatingDelay = ParseStringValue(trainData, "term_delay"),
                IsDirect = ParseBooleanValue(trainData, "is_direct")
            };
            nextTrainsToArrive.Add(train);
        }

        return nextTrainsToArrive;
    }

    public string GtfsNameToApiName(string apiName)
    {
        string stopName;

        switch (apiName)
        {
            case "Gray 30th Street":
                stopName = "30th Street Station";
                break;
            case "49th Street":
                stopName = "49th St";
                break;
            case "Airport Terminal E F":
                stopName = "Airport Terminal E-F";
                break;
            case "Airport Terminal C D":
                stopName = "Airport Terminal C-D";
                break;
            case "Chester":
                stopName = "Chester TC";
                break;
            case "9th Street Landsdale":
                stopName = "9th St";
                break;
            case "Fort Washington":
                stopName = "Ft Washington";
                break;
            case "Norristown - Elm Street":
                stopName = "Elm St";
                break;
            default:
                stopName = apiName;
                break;
        }

        return stopName;
    }
}
