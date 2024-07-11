using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models.Components;

public class AlertsViewModel
{
    public string AgencyName { get; set; } = null!;
    public List<RouteAlert> SeptaAlerts { get; set; }

    public AlertsViewModel()
    {
        SeptaAlerts = new List<RouteAlert>();
    }
}