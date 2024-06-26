namespace DotNetPIS.Domain.Models.GTFS;

public class TripShape
{
    public int TripId { get; set; }
    public Trip? Trip { get; set; }

    public int ShapeId { get; set; }
    public Shape? Shape { get; set; }

    public string Id
    {
        get => TripId + "" + ShapeId;
        set => throw new InvalidOperationException("Setter Not Allowed");
    }
    
    public override bool Equals(object obj)
    {
        if (obj is TripShape other)
        {
            return TripId == other.TripId && ShapeId == other.ShapeId;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(TripId, ShapeId);
    }
}