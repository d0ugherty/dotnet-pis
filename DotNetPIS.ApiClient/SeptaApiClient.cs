using System.Diagnostics;
using DotNetPIS.Domain.Interfaces;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.ApiClient;

public class SeptaApiClient : ISeptaApiClient
{
    private readonly HttpClient _httpClient;

    public SeptaApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JObject> GetRegionalRailArrivals(string stationName, int results = 10)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}");

        JObject data = JObject.Parse(await response.Content.ReadAsStringAsync());

        return data;
    }

    public async Task<JToken> RegionalRailTrainView()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("https://www3.septa.org/api/TrainView/index.php");
        
        response.EnsureSuccessStatusCode();
        
        JToken data = JToken.Parse(await response.Content.ReadAsStringAsync());
        
        return data;
    }

    public async Task<JObject> GetRegionalRailSchedule(string trainNumber)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/RRSchedules/index.php?req1={trainNumber}");
        
        response.EnsureSuccessStatusCode();

        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> GetNextToArrive(string startStation, string endStation, int results = 5)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/NextToArrive/index.php?req1={startStation}&req2={endStation}&req3={results}");
        
        response.EnsureSuccessStatusCode();
        
        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> TransitView(string routeNumber)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"https://www3.septa.org/api/TransitView/index.php?route={routeNumber}");
        
        response.EnsureSuccessStatusCode();
        
        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> TransitViewAll()
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync("https://www3.septa.org/api/TransitViewAll/index.php");

        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JToken> GetAlertData(string routeId = "")
    {
        HttpResponseMessage response;
        
        if (routeId != "")
        { 
            response = await _httpClient.GetAsync($"https://www3.septa.org/api/Alerts/get_alert_data.php?route_id={routeId}");
        }
        else
        {
            response = await _httpClient.GetAsync($"https://www3.septa.org/api/Alerts/get_alert_data.php");
        }

        JToken data = JToken.Parse(await response.Content.ReadAsStringAsync());;

        return data;
    }
    
    private static async Task<JObject> ParseResponse(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        
        JObject data = new JObject();

        try
        {
            data = JObject.Parse(responseContent);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        return data;
    }
    
    
}
