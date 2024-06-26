using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class ShapeService
{
    private readonly IRepository<Shape, int> _shapeRepo;
    private readonly IRepository<TripShape, string> _tripShapeRepo;
    private readonly IRepository<Trip, int> _tripRepo;

    public ShapeService(IRepository<Shape, int> shapeRepo, IRepository<TripShape, string> tripShapeRepo, IRepository<Trip, int> tripRepo)
    {
        _shapeRepo = shapeRepo;
        _tripShapeRepo = tripShapeRepo;
        _tripRepo = tripRepo;
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

    public async Task<List<Shape>> GetShapesByRouteList(List<Route> routes)
    {
        List<Shape> shapes = new List<Shape>();
            
        foreach (var route in routes)
        {
            List<Shape> routeShapes = await GetShapesByRoute(route.Id);
            shapes.AddRange(routeShapes);
        }

        return shapes;
    }
}