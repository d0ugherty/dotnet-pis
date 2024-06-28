using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.Domain.Services;

public class AlertService : BaseService
{
    private readonly ISeptaApiClient _septApiClient;

    public AlertService(ISeptaApiClient septApiClient)
    {
        _septApiClient = septApiClient;
    }

    public Task<List<RouteAlert>> GetSeptaRouteAlert(string routeId)
    {
        throw new NotImplementedException();
    }
}