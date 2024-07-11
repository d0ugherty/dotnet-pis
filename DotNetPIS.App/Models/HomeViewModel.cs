using DotNetPIS.Domain.Models.GTFS;

namespace DotNetPIS.App.Models;

public class HomeViewModel
{
    public List<Source> InfoSources { get; set; }

    public Source? SelectedSource { get; set; }

    public HomeViewModel()
    {
        InfoSources = new List<Source>();
    }
}
