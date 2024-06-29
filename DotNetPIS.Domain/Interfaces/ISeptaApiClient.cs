using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Interfaces;

public interface ISeptaApiClient
{
    Task<JObject> GetRegionalRailArrivals(string stationName, int results);
    Task<JToken> RegionalRailTrainView();
    Task<JObject> GetRegionalRailSchedule(string trainNumber);
    Task<JObject> GetNextToArrive(string startStation, string endStation, int results);
    Task<JObject> TransitView(string routeNumber);
    Task<JToken> GetAlertData(string routeString = "");
}