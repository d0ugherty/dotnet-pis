using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models;

public class HomeViewModel
{
    public string AgencyName { get; set; }
    public List<RouteAlert> SeptaAlerts { get; set; } = new List<RouteAlert>();
}