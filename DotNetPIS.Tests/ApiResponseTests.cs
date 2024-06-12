using System.Diagnostics;
using System.Net;
using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace DotNetPIS.Tests;

public class ApiResponseTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly HttpClient _httpClient;
    
    
    public ApiResponseTests(ITestOutputHelper testOutputHelper)
    {
        _httpClient = new HttpClient(); 
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("30th Street Station", "N", 10)]
    public async Task SeptaApi_SuccessStatusCode(string stationName, string direction, int results)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}&direction={direction}");

        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("30th Street Station", "N", 10)]
    public async Task SeptaApi_HasContent(string stationName, string direction, int results)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}&direction={direction}");
        
        string responseContent = await response.Content.ReadAsStringAsync();

        Assert.NotNull(responseContent);
    }

    [Theory]
    [InlineData("30th Street Station", "N", 10)]
    public async Task SeptaApi_ParseContent(string stationName, string direction, int results)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}&direction={direction}");
        
        string responseContent = await response.Content.ReadAsStringAsync();
        
        JObject data = JObject.Parse(responseContent);

        Assert.NotEmpty(data);
    }
    
    [Theory]
    [InlineData("30th Street Station", "N", 10)]
    public async void ParseJObjectToStationDeparture_ShouldParseCorrectly(string stationName, string direction, int results)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"https://www3.septa.org/api/Arrivals/index.php?station={stationName}&results={results}&direction={direction}");

        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        
        JObject root = JObject.Parse(content);
        
        var stationArrivals = ParseJObjectToStationDeparture(root);

        var firstArrival = stationArrivals[0];
        
        _testOutputHelper.WriteLine(firstArrival.ToString());
    }

    private List<Arrival> ParseJObjectToStationDeparture(JObject root)
    {
        var stationArrivals = new List<Arrival>();
        var stationData = root.Properties().First();

        var northboundArrivals = stationData.Value[0]?["Northbound"];
        
        _testOutputHelper.WriteLine($"northboundArrivals = {northboundArrivals}");
        
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
                    SchedTime = DateTime.Parse(arrival["sched_time"]?.ToString() ?? throw new InvalidOperationException()).ToShortTimeString(),
                    DepartTime = DateTime.Parse(arrival["depart_time"]?.ToString() ?? throw new InvalidOperationException()).ToShortTimeString(),
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