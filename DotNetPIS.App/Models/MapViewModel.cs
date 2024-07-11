using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models;

public class MapViewModel
{
    public Source InfoSource { get; set; }

    public List<TrainView>? SeptaTrainMarkers { get; set; }
    public List<Stop> Stops { get; set; }
    public List<Shape> Shapes { get; set; }

    public RouteType RouteType { get; set; }

    public bool GetTrainData { get; set; }
    public bool GetBusData { get; set; }
    public bool GetTrolleyData { get; set; }

    public MapViewModel()
    {
        SeptaTrainMarkers = new List<TrainView>();
        Stops = new List<Stop>();
        Shapes = new List<Shape>();
    }
}