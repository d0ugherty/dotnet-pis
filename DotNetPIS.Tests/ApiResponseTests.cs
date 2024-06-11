using System.Diagnostics;
using System.Net;
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
}