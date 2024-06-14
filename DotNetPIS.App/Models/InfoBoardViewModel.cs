using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetPIS.App.Models;

public class InfoBoardViewModel
{
    public string Title { get; set; }
    public string StationName { get; set; }
    public List<Arrival> Arrivals { get; set; } = new List<Arrival>();
    public List<SelectListItem>? Stops { get; set; }
    public int StopId { get; set; }
}
