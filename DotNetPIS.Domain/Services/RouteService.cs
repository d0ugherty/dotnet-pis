using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class RouteService
{
    private readonly IRepository<Route, int> _routeRepo;

    public RouteService(IRepository<Route, int> routeRepo)
    {
        _routeRepo = routeRepo;
    }

    public async Task<List<Route>> GetRoutesByAgencyAndType(string agencyName, int routeType)
    {
        List<Route> routes = await _routeRepo.GetAll()
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals(agencyName) 
                            && route.Type == routeType)
            .ToListAsync();

        return routes;
    }
}