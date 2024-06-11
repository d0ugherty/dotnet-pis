namespace DotNetPIS.Domain.Models.SEPTA;

public class Arrival
{
    public string? Direction { get; set; }
    public string? Path { get; set; }
    public string? TrainId { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public string? Line { get; set; }
    public string? Status { get; set; }
    public string? ServiceType { get; set; }
    public string? NextStation { get; set; }
    public DateTime? SchedTime { get; set; }
    public DateTime? DepartTime { get; set; }
    public string? Track { get; set; }
    public string? TrackChange { get; set; }
    public string? Platform { get; set; }
    public string? PlatformChange { get; set; }
}