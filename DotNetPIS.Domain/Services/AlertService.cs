using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.SEPTA;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class AlertService : BaseService
{
    private readonly ISeptaApiClient _septApiClient;

    public AlertService(ISeptaApiClient septApiClient)
    {
        _septApiClient = septApiClient;
    }

    public async Task<List<RouteAlert>> GetSeptaRouteAlerts(string routeId, AlertType alertType)
    {
        var routeAlerts = new List<RouteAlert>();
        
        string routeString = $"{alertType}_route_{routeId}";

        JToken response = await _septApiClient.GetAlertData(routeString);

        foreach (var alertData in response)
        {
            var alert = new RouteAlert
            {
                RouteId = ParseStringValue(alertData, "route_id"),
                RouteName = ParseStringValue(alertData, "route_name"),
                CurrentMessage = ParseStringValue(alertData, "current_message"),
                AdvisoryId = ParseStringValue(alertData, "advisory_id"),
                AdvisoryMessage = ParseStringValue(alertData, "advisory_message"),
                DetourMessage = ParseStringValue(alertData, "detour_message"),
                DetourStartLocation = ParseStringValue(alertData, "detour_start_location"),
                DetourStartDateTime = ParseStringValue(alertData, "detour_start_data_time"),
                DetourEndDateTime = ParseStringValue(alertData, "detour_end_date_time"),
                DetourReason = ParseStringValue(alertData, "detour_reason"),
                LastUpdated = ParseStringValue(alertData, "last_updated"),
                IsSnow = ParseBooleanValue(alertData, "isSnow")
            };
            
            routeAlerts.Add(alert);
        }
        
        return routeAlerts;
    }

    public async Task<List<RouteAlert>> GetAllSeptaAlerts()
    {
        var routeAlerts = new List<RouteAlert>();
        
        JToken response = await _septApiClient.GetAlertData();

        foreach (var alertData in response)
        {
            var alert = new RouteAlert
            {
                RouteId = ParseStringValue(alertData, "route_id"),
                RouteName = ParseStringValue(alertData, "route_name"),
                CurrentMessage = ParseStringValue(alertData, "current_message"),
                AdvisoryId = ParseStringValue(alertData, "advisory_id"),
                AdvisoryMessage = ParseStringValue(alertData, "advisory_message"),
                DetourMessage = ParseStringValue(alertData, "detour_message"),
                DetourStartLocation = ParseStringValue(alertData, "detour_start_location"),
                DetourStartDateTime = ParseStringValue(alertData, "detour_start_data_time"),
                DetourEndDateTime = ParseStringValue(alertData, "detour_end_date_time"),
                DetourReason = ParseStringValue(alertData, "detour_reason"),
                LastUpdated = ParseStringValue(alertData, "last_updated"),
                IsSnow = ParseBooleanValue(alertData, "isSnow")
            };
            
            routeAlerts.Add(alert);
        }
        
        return routeAlerts;
    }
    
}