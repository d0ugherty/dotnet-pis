using DotNetPIS.App.Models.Components;
using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models;

public class InfoBoardViewModel
{
    public string Title { get; set; } = null!;
    public string StationName { get; set; } = null!;
    public string AgencyName { get; set; } = null!;
    
    public int StopId { get; set; }
    
    public List<Arrival> Arrivals { get; set; }
    
    public StopSelectViewModel StopSelectViewModel { get; set; }
    
    public AlertsViewModel AlertsViewModel { get; set; }

    public InfoBoardViewModel()
    {
        Arrivals =  new List<Arrival>();
    }
}
