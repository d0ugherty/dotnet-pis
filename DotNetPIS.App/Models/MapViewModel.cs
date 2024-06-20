using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;

namespace DotNetPIS.App.Models;

public class SeptaMapViewModel
{
    public List<TrainView>? SeptaTrainMarkers { get; set; }
    public List<Shape>? Shapes { get; set; }
}