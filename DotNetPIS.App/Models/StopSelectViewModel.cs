using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetPIS.App.Models;

public class StopSelectViewModel
{
    public string SelectController { get; set; } = null!;
    public string SelectAction { get; set; } = null!;
    public string AgencyName { get; set; } = null!;
    public int StopId { get; set; }
    public IEnumerable<SelectListItem> Stops { get; set; } = new List<SelectListItem>();
}