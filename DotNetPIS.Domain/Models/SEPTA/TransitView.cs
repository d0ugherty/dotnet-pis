namespace DotNetPIS.Domain.Models.SEPTA;

public class TransitView
{
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public string? Label { get; set; }
    public string? VehicleId { get; set; }
    public string? BlockId { get; set; }
    public string? Direction { get; set; }
    public string? Destination { get; set; }
    public string? Offset { get; set; }
    public int? Heading { get; set; }
    public int? MinutesLate { get; set; }
    public int? OriginalMinutesLate { get; set; }
    public string? Trip { get; set; }
    public string? NextStopId { get; set; }
    public string? NextStopName { get; set; }
    public int? NextStopSequence { get; set; }
    public string? SeatAvailability { get; set; }
    public int? TimeStamp { get; set; }
}