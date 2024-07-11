namespace DotNetPIS.Domain.Models.SEPTA;

public class RouteAlert
{
    public string? RouteId { get; set; }
    public string? RouteName { get; set; }
    public string? CurrentMessage { get; set; }
    public string? AdvisoryId { get; set; }
    public string? AdvisoryMessage { get; set; }
    public string? DetourMessage { get; set; }
    public string? DetourStartLocation { get; set; }
    public string? DetourStartDateTime { get; set; }
    public string? DetourEndDateTime { get; set; }
    public string? DetourReason { get; set; }
    public string? LastUpdated { get; set; }
    public bool? IsSnow { get; set; }
}
