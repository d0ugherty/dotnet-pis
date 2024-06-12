using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class SeptaRegionalRailService
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
                Direction = trainData["direction"]?.ToString(),
                Path = trainData["path"]?.ToString(),
                TrainId = trainData["train_id"]?.ToString(),
                Origin = trainData["origin"]?.ToString(),
                Destination = trainData["destination"]?.ToString(),
                Line = trainData["line"]?.ToString(),
                Status = trainData["status"]?.ToString(),
                ServiceType = trainData["service_type"]?.ToString(),
                NextStation = trainData["next_station"]?.ToString(),
                SchedTime = DateTime.Parse(trainData["sched_time"]?.ToString() ?? string.Empty),
                DepartTime = DateTime.Parse(trainData["depart_time"]?.ToString() ?? string.Empty),
                Track = trainData["track"]?.ToString(),
                TrackChange = trainData["track_change"]?.ToString(),
                Platform = trainData["platform"]?.ToString(),
                PlatformChange = trainData["platform_change"]?.ToString()
            };
            stationArrivals.Add(arrival);
        }
        return stationArrivals;
    }
}