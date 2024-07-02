using System.Globalization;
using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class InfoBoardService : BaseService
{
    private readonly ISeptaApiClient _septaApiClient;

    public InfoBoardService(ISeptaApiClient septaApiClient)
    {
        _septaApiClient = septaApiClient;
    }

    public async Task<List<Arrival>> GetRegionalRailArrivals(string stationName, int results = 10)
    {
        var stationArrivals = new List<Arrival>();
        
        string[] directions = { "Northbound", "Southbound" };
        
        JObject response = await _septaApiClient.GetRegionalRailArrivals(stationName, results);

        JProperty data = response.Properties().First();

        int dataLength = data.Value.Count();

        for (int idx = 0; idx < dataLength; idx++)
        {
            JToken arrivals = data.Value[idx]?[directions[idx]] ?? throw new InvalidOperationException();
            
            foreach (var trainData in arrivals)
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
        }
        stationArrivals = SortByTime(stationArrivals);
        
        return stationArrivals;
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

    private List<Arrival> SortByTime(List<Arrival> arrivals)
    {
        List<Arrival> sortedList = arrivals
            .OrderBy(a => DateTime.ParseExact(a.DepartTime, "h:mm tt", CultureInfo.InvariantCulture))
            .ToList();

        return sortedList;
    }
}



