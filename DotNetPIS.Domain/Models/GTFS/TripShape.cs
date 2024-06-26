namespace DotNetPIS.Domain.Models.GTFS;

public class TripShape
{
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    public int ShapeId { get; set; }
    public Shape Shape { get; set; } = null!; 
}