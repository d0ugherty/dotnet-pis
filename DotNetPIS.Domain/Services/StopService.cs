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

    public async Task<List<Stop>> GetStopsByAgencyAndRouteType(int agencyId, int routeType)
    {
        List<Stop> stops = await _stopRepo.GetAll()
            .Where(s => s.StopTimes.Any(st => st.Trip.Route.AgencyId == agencyId && st.Trip.Route.Type == routeType))
            .Distinct()
            .ToListAsync();

        return stops;
    }
}