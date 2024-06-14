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

    public async Task<JObject> GetRegionalRailArrivals(string stationName, string direction, int results = 10)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}&direction={direction}");

        JObject data = await ParseResponse(response);
         
        return data;
    }

    public async Task<JObject> RegionalRailTrainView()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("https://www3.septa.org/api/TrainView/index.php");

        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> GetRegionalRailSchedule(string trainNumber)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/RRSchedules/index.php?req1={trainNumber}");

        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> GetNextToArrive(string startStation, string endStation, int results = 5)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/NextToArrive/index.php?req1={startStation}&req2={endStation}&req3={results}");

        JObject data = await ParseResponse(response);

        return data;
    }

    public async Task<JObject> TransitView(string routeNumber)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"https://www3.septa.org/api/TransitView/index.php?route={routeNumber}");

        JObject data = await ParseResponse(response);

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
