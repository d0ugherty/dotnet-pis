using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Domain.Services;

public class ShapeService
{
    private readonly IRepository<Shape, int> _shapeRepo;

    public ShapeService(IRepository<Shape, int> shapeRepo)
    {
        _shapeRepo = shapeRepo;
    }

    public async Task<List<Shape>> GetShapesByDataSource(string sourceName)
    {
        List<Shape> shapes = await _shapeRepo.GetAll()
            .Where(shape => shape.Source.Equals(sourceName))
            .ToListAsync();

        return shapes;
    }
}