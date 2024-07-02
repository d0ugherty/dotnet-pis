using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class RouteService
{
    private readonly IRepository<Route, int> _routeRepo;
    private readonly IRepository<Stop, int> _stopRepo;

    public RouteService(IRepository<Route, int> routeRepo, IRepository<Stop, int> stopRepo)
    {
        _routeRepo = routeRepo;
        _stopRepo = stopRepo;
    }

    public async Task<List<Route>> GetRoutesByAgencyAndType(string agencyName, RouteType routeType)
    {
        List<Route> routes = await _routeRepo.GetAll()
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals(agencyName) 
                            && route.Type == (int)routeType)
            .ToListAsync();

        return routes;
    }
}