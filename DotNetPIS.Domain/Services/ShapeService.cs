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

    public async Task<List<Shape>> GetShapesByDataSource(string sourceName)
    {
        List<Shape> shapes = await _shapeRepo.GetAll()
            .Where(shape => shape.Source.Equals(sourceName))
            .ToListAsync();

        return shapes;
    }
    
    public async Task<List<Shape>> GetShapesByRoute(int routeId)
    {
        var shapes = new List<Shape>();
        
        List<Trip> routeTrips = await _tripRepo.GetAll()
            .Where(trip => trip.RouteId == routeId)
            .Distinct()
            .ToListAsync();
        
        foreach (var trip in routeTrips)
        {
            List<Shape> tripShapes = _tripShapeRepo.GetAll()
                .Where(ts => ts.TripId == trip.Id)
                .Select(ts => ts.Shape)
                .ToList()!;
            
            shapes.AddRange(tripShapes);
        }
        return shapes;
    }
}