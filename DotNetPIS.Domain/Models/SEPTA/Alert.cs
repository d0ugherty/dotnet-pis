namespace DotNetPIS.Domain.Models.SEPTA;

public class Alert
{
    public string? Route { get; set; }
    public string? RouteId { get; set; }
    public string? RouteName { get; set; }
    public string? Sequence { get; set; }
    public string? Mode { get; set; }
    public bool IsAdvisory { get; set; }
    public bool IsDetour { get; set; }
    public bool IsAlert { get; set; }
    public bool IsSuspended { get; set; }
    public bool IsElevator { get; set; }
    public bool IsStrike { get; set; }
    public bool IsModifiedService { get; set; }
    public bool IsDelays { get; set; }
    public bool IsDiversion { get; set; }
    public bool IsDetourAlert { get; set; }
    public bool IsSnow { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? Description { get; set; }
    public string? AlertText { get; set; }
    public string? Advisory { get; set; }
    public List<Detour>? Detour { get; set; }
    public List<string>? Elevator { get; set; }
}