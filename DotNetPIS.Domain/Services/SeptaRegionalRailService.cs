using DotNetPIS.Domain.Interfaces;
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
                    Direction = GetStringValue(trainData, "direction"),
                    Path = GetStringValue(trainData, "path"),
                    TrainId = RemoveSpecialCharacters(GetStringValue(trainData, "train_id")),
                    Origin = GetStringValue(trainData, "origin"),
                    Destination = GetStringValue(trainData, "destination"),
                    Line = GetStringValue(trainData, "line"),
                    Status = GetStringValue(trainData, "status"),
                    ServiceType = GetStringValue(trainData, "service_type"),
                    NextStation = GetStringValue(trainData, "next_station"),
                    SchedTime = GetDateTimeValue(trainData, "sched_time"),
                    DepartTime = GetDateTimeValue(trainData, "depart_time"),
                    Track = GetStringValue(trainData, "track"),
                    TrackChange = GetStringValue(trainData, "track_change"),
                    Platform = GetStringValue(trainData, "platform"),
                    PlatformChange = GetStringValue(trainData, "platform_change")
                };
            stationArrivals.Add(arrival);
        }
        return stationArrivals;
    }

    public async Task<List<TrainView>> GetTrainView()
    {
        JObject response = await _septaApiClient.RegionalRailTrainView();

        JProperty data = response.Properties().First();

        var trainsOnSystem = new List<TrainView>();
        
        JToken trainView = data.Value;

        foreach (var trainData in trainView)
        {
            var train = new TrainView
            {
                Latitude = GetFloatValue(trainData, "lat"),
                Longitude = GetFloatValue(trainData, "lon"),
                TrainNumber = RemoveSpecialCharacters(GetStringValue(trainData, "trainno")),
                ServiceType = GetStringValue(trainData, "service"),
                Destination = GetStringValue(trainData, "destination"),
                CurrentStop = GetStringValue(trainData, "currentstop"),
                NextStop = GetStringValue(trainData, "nextstop"),
                Line = GetStringValue(trainData, "line"),
                Consist = GetStringValue(trainData, "consist"),
                Heading = GetFloatValue(trainData, "heading"),
                MinutesLate = GetIntValue(trainData, "late"),
                Source = GetStringValue(trainData, "SOURCE"),
                Track = GetStringValue(trainData, "TRACK"),
                TrackChange = GetStringValue(trainData, "TRACK_CHANGE")
            };
            trainsOnSystem.Add(train);
        }
        return trainsOnSystem;
    }
}
