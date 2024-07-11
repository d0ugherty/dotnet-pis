using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models;

public class InfoBoardViewModel
{
    public string Title { get; set; } = null!;
    public string StationName { get; set; } = null!;
    public string AgencyName { get; set; } = null!;
    
    public int StopId { get; set; }
    
    public List<Arrival> Arrivals { get; set; }
    //public List<SelectListItem>? Stops { get; set; }
    public List<RouteAlert> SeptaAlerts { get; set; }
    
    public StopSelectViewModel StopSelectViewModel { get; set; }

    public InfoBoardViewModel()
    {
        Arrivals =  new List<Arrival>();
       // Stops = new List<SelectListItem>();
        SeptaAlerts = new List<RouteAlert>();
    }
}
