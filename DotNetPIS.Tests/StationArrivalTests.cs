using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;


namespace DotNetPIS.Tests;

public class StationArrivalTests
{
    [Fact]
    public void ParseJObjectToStationDeparture_ShouldParseCorrectly()
    {
        string json = @"
        {
            ""Gray 30th Street Departures: June 11, 2024, 4:38 pm"": {
                ""Northbound"": [
                    {
                        ""direction"": ""N"",
                        ""path"": ""R5/4N"",
                        ""train_id"": ""5438"",
                        ""origin"": ""Thorndale"",
                        ""destination"": ""Glenside"",
                        ""line"": ""Warminster"",
                        ""status"": ""3 min"",
                        ""service_type"": ""LOCAL"",
                        ""next_station"": ""30th Street Gray"",
                        ""sched_time"": ""2024-06-11 16:39:01.000"",
                        ""depart_time"": ""2024-06-11 16:40:00.000"",
                        ""track"": ""1"",
                        ""track_change"": null,
                        ""platform"": """",
                        ""platform_change"": null
                    },
                    {
                        ""direction"": ""N"",
                        ""path"": ""R5N"",
                        ""train_id"": ""6594"",
                        ""origin"": ""Gray 30th Street"",
                        ""destination"": ""Doylestown"",
                        ""line"": ""Lansdale/Doylestown"",
                        ""status"": ""2 min"",
                        ""service_type"": ""LOCAL"",
                        ""next_station"": null,
                        ""sched_time"": ""2024-06-11 16:47:00.000"",
                        ""depart_time"": ""2024-06-11 16:48:00.000"",
                        ""track"": ""2"",
                        ""track_change"": null,
                        ""platform"": """",
                        ""platform_change"": null
                    }
                ]
            }
        }";

        JObject root = JObject.Parse(json);

        var stationArrivals = ParseJObjectToStationDeparture(root);

        Assert.Equal(2, stationArrivals.Count);

        var firstArrival = stationArrivals[0];
        Assert.Equal("N", firstArrival.Direction);
        Assert.Equal("R5/4N", firstArrival.Path);
        Assert.Equal("5438", firstArrival.TrainId);
        Assert.Equal("Thorndale", firstArrival.Origin);
        Assert.Equal("Glenside", firstArrival.Destination);
        Assert.Equal("Warminster", firstArrival.Line);
        Assert.Equal("3 min", firstArrival.Status);
        Assert.Equal("LOCAL", firstArrival.ServiceType);
        Assert.Equal("30th Street Gray", firstArrival.NextStation);
        Assert.Equal(new DateTime(2024, 6, 11, 16, 39, 1).ToShortTimeString(), firstArrival.SchedTime);
        Assert.Equal(new DateTime(2024, 6, 11, 16, 40, 0).ToShortTimeString(), firstArrival.DepartTime);
        Assert.Equal("1", firstArrival.Track);
        Assert.Equal("", firstArrival.Platform);

        var secondArrival = stationArrivals[1];
        Assert.Equal("N", secondArrival.Direction);
        Assert.Equal("R5N", secondArrival.Path);
        Assert.Equal("6594", secondArrival.TrainId);
        Assert.Equal("Gray 30th Street", secondArrival.Origin);
        Assert.Equal("Doylestown", secondArrival.Destination);
        Assert.Equal("Lansdale/Doylestown", secondArrival.Line);
        Assert.Equal("2 min", secondArrival.Status);
        Assert.Equal("LOCAL", secondArrival.ServiceType);
        Assert.Equal(new DateTime(2024, 6, 11, 16, 47, 0).ToShortTimeString(), secondArrival.SchedTime);
        Assert.Equal(new DateTime(2024, 6, 11, 16, 48, 0).ToShortTimeString(), secondArrival.DepartTime);
        Assert.Equal("2", secondArrival.Track);
    }

    private List<Arrival> ParseJObjectToStationDeparture(JObject root)
    {
        var stationArrivals = new List<Arrival>();
        var stationData = root.Properties().First();

        var northboundArrivals = stationData.Value["Northbound"];
        if (northboundArrivals != null)
            foreach (var arrival in northboundArrivals)
            {
                var trainArrival = new Arrival
                {
                    Direction = arrival["direction"]?.ToString(),
                    Path = arrival["path"]?.ToString(),
                    TrainId = arrival["train_id"]?.ToString(),
                    Origin = arrival["origin"]?.ToString(),
                    Destination = arrival["destination"]?.ToString(),
                    Line = arrival["line"]?.ToString(),
                    Status = arrival["status"]?.ToString(),
                    ServiceType = arrival["service_type"]?.ToString(),
                    NextStation = arrival["next_station"]?.ToString(),
                    DepartTime = DateTime.Parse(arrival["depart_time"]?.ToString() ?? string.Empty).ToShortTimeString(),
                    Track = arrival["track"]?.ToString(),
                    TrackChange = arrival["track_change"]?.ToString(),
                    Platform = arrival["platform"]?.ToString(),
                    PlatformChange = arrival["platform_change"]?.ToString()
                };
                stationArrivals.Add(trainArrival);
            }

        return stationArrivals;
    }
}
