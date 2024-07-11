using Newtonsoft.Json.Linq;

namespace DotNetPIS.ApiClient;

public class MbtaApiClient
{
    private readonly HttpClient _httpClient;

    public MbtaApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JToken> GetVehicles(int routeType)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"https://api-v3.mbta.com/vehicles?filter%5Broute_type%5D={routeType}");

        JToken data = JToken.Parse(await response.Content.ReadAsStringAsync());

        return data;
    }

    public async Task<JToken> GetShapes(string routeId)
    {
        HttpResponseMessage response =
            await _httpClient.GetAsync($"https://api-v3.mbta.com/shapes?filter%5Broute%5D={routeId}");
        
        JToken data = JToken.Parse(await response.Content.ReadAsStringAsync());

        return data;
    }
}