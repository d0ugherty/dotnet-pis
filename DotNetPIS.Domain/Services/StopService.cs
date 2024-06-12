using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class StopService
{
    private readonly IRepository<Stop, int> _stopRepo;

    public StopService(IRepository<Stop, int> stopRepo)
    {
        _stopRepo = stopRepo;
    }

    public async Task<List<Stop>> GetStopsByAgencyAndRouteType(string agencyName, int routeType)
    {
        List<Stop> stops = await _stopRepo.GetAll()
            .Where(s => s.StopTimes.Any(st => st.Trip.Route.Agency != null && 
                                              st.Trip.Route.Agency.Name.Equals(agencyName) 
                                              && st.Trip.Route.Type == routeType))
            .Distinct()
            .ToListAsync();

        return stops;
    }
}