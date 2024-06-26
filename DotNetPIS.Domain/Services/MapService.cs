using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class MapService
{
    private readonly IRepository<Trip, int> _tripRepo;
    private readonly IRepository<TripShape, string> _tripShapeRepo;
    private readonly IRepository<Route, int> _routeRepo;

    public MapService(IRepository<Trip, int> tripRepo, IRepository<TripShape, string> tripShapeRepo, IRepository<Route, int> routeRepo)
    {
        _tripRepo = tripRepo;
        _tripShapeRepo = tripShapeRepo;
        _routeRepo = routeRepo;
    }
    
    public async Task<List<Shape>> GetShapesByRoute(int routeId)
    {
        Trip routeTrip = await _tripRepo.GetAll()
                             .FirstOrDefaultAsync(trip => trip.RouteId == routeId) 
                         ?? throw new InvalidOperationException($"No trips found for route ID {routeId}");

        List<Shape> shapes = await _tripShapeRepo.GetAll()
            .Where(ts => ts.TripId == routeTrip.Id)
            .Select(ts => ts.Shape)
            .ToListAsync();
        
        return shapes;
    }

    public async Task<List<Shape>> GetShapeData(RouteType routeType, string agencyName)
    {
        List<Route> routes = await _routeRepo.GetAll()
            .Where(route => route.Agency != null 
                            && route.Agency.Name.Equals(agencyName) 
                            && route.Type == (int)routeType)
            .ToListAsync();
        
        List<Shape> shapes = new List<Shape>();
            
        foreach (var route in routes)
        {
            List<Shape> routeShapes = await GetShapesByRoute(route.Id);
            shapes.AddRange(routeShapes);
        }
        
        return shapes;
    }
    
}