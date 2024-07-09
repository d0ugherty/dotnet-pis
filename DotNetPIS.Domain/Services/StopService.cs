using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetPIS.Domain.Services;

public class StopService
{
    private readonly IRepository<Stop, int> _stopRepo;

    public StopService(IRepository<Stop, int> stopRepo)
    {
        _stopRepo = stopRepo;
    }
    
    public async Task<List<Stop>> GetStopsByAgencyAndRouteType(string agencyName, RouteType routeType)
    {
        List<Stop> stops = await _stopRepo.GetAll()
            .Where(s => s.StopTimes.Any(st => st.Trip.Route.Agency != null &&
                                              st.Trip.Route.Agency.Name.Equals(agencyName)
                                              && st.Trip.Route.Type == (int)routeType))
            .Distinct()
            .ToListAsync();

        return stops;
    }

    public async Task<List<SelectListItem>> GetStopSelectList(string agencyName, RouteType routeType)
    {
        List<Stop> stops = await GetStopsByAgencyAndRouteType(agencyName, routeType);

        List<SelectListItem> stopSelectList = stops.Select(stop => new SelectListItem
        {
            Value = stop.Id.ToString(),
            Text = stop.Name
        }).ToList();

        return stopSelectList;
    }

    public async Task<Stop> GetStopById(int id)
    {
        return await Task.FromResult(_stopRepo.GetById(id));
    }

    public async Task<Stop> GetStopByNameAndAgency(string stationName, string agencyName)
    {
        Stop stop = await _stopRepo.GetAll()
            .FirstOrDefaultAsync(stop => stop.Name.Equals(stationName) && stop.Source.Name.Equals(agencyName)) 
                    ?? throw new InvalidOperationException($"No Stop entity found with name {stationName} and Source.Name {agencyName}");

        return stop;
    }
}