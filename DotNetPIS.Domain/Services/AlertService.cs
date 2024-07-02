using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public class AlertService : BaseService
{
    private readonly ISeptaApiClient _septApiClient;
    private readonly IRepository<Stop, int> _stopRepo;
    private readonly IRepository<Trip, int> _tripRepo;
    private readonly IRepository<StopTime, int> _stopTimeRepo;

    public AlertService(ISeptaApiClient septApiClient, IRepository<Stop, int> stopRepo, IRepository<Trip, int> tripRepo, IRepository<StopTime, int> stopTimeRepo)
    {
        _septApiClient = septApiClient;
        _stopRepo = stopRepo;
        _tripRepo = tripRepo;
        _stopTimeRepo = stopTimeRepo;
    }

    public async Task<List<RouteAlert>> GetSeptaRouteAlerts(string routeId, AlertType alertType)
    {
        string routeString = $"{alertType}_route_{routeId}";

        JToken response = await _septApiClient.GetAlertData(routeString);

        List<RouteAlert> routeAlerts = ParseResponse(response);
        
        return routeAlerts;
    }

    public async Task<List<RouteAlert>> GetAllSeptaAlerts()
    {
        JToken response = await _septApiClient.GetAlertData();

        List<RouteAlert> allAlerts = ParseResponse(response);
        
        return allAlerts;
    }
    
    public async Task<List<RouteAlert>> GetSeptaStopAlerts(string stopName)
    {
        var stopAlerts = new List<RouteAlert>();
        
        Stop septaStop = await _stopRepo.GetAll()
                             .FirstOrDefaultAsync(stop => stop.Name.Equals(stopName) && stop.Source.Name.Equals("SEPTA")) 
                         ?? throw new InvalidOperationException($"No stops found with name {stopName} for SEPTA");

        List<Route> stopRoutes = await _stopTimeRepo.GetAll()
            .Where(stopTime => stopTime.StopId == septaStop.Id)
            .GroupBy(stopTime => stopTime.Trip.RouteId)
            .Select(group => group.First().Trip.Route)
            .ToListAsync();

        foreach (var route in stopRoutes)
        {
            string routeId = route.RouteNumber;

            AlertType alertType = ToSeptaAlertType(route.Type);

            List<RouteAlert> alerts = await GetSeptaRouteAlerts(routeId, alertType);
            
            stopAlerts.AddRange(alerts);
        }

        return stopAlerts;
    }

    private List<RouteAlert> ParseResponse(JToken response)
    {
        var routeAlerts = new List<RouteAlert>();

        foreach (var alertData in response)
        {
            var alert = new RouteAlert
            {
                RouteId = ParseStringValue(alertData, "route_id"),
                RouteName = ParseStringValue(alertData, "route_name"),
                CurrentMessage = ParseStringValue(alertData, "current_message"),
                AdvisoryId = ParseStringValue(alertData, "advisory_id"),
                AdvisoryMessage = DecodeHtmlString(alertData, "advisory_message"),
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

    private static AlertType ToSeptaAlertType(int routeType)
    {
        return routeType switch
        {
            0 => AlertType.Bus,
            1 => AlertType.Rr,
            2 => AlertType.Trolley,
            3 => AlertType.Rr,
            _ => AlertType.Bus
        };
    }
}