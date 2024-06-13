namespace DotNetPIS.Domain.Models.SEPTA;

public class TrainView
{
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public string? TrainNumber { get; set; }
    public string? ServiceType { get; set; }
    public string? Destination { get; set; }
    public string? CurrentStop { get; set; }
    public string? NextStop { get; set; }
    public string? Line { get; set; }
    public string? Consist { get; set; }
    public float? Heading { get; set; }
    public int? MinutesLate { get; set; }
    public string? Source { get; set; }
    public string? Track { get; set; }
    public string? TrackChange { get; set; }
}