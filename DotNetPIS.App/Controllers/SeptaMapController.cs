using DotNetPIS.App.Models;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPIS.App.Controllers
{
    public class SeptaMapController : Controller
    {

        private readonly StopService _stopService;
        private readonly ShapeService _shapeService;
        private readonly SeptaRegionalRailService _septaRrService;

        public SeptaMapController(StopService stopService, ShapeService shapeService, SeptaRegionalRailService septaRrService)
        {
            _stopService = stopService;
            _shapeService = shapeService;
            _septaRrService = septaRrService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderMap()
        {
            throw new NotImplementedException();
        }

        private async Task<List<TrainView>> GetTrainData()
        {
            List<TrainView> trainMarkers = await _septaRrService.GetTrainView();

            return trainMarkers;
        }

        private async Task<List<Stop>> GetTrainStopData()
        {
            List<Stop> stops = await _stopService.GetStopsByAgencyAndRouteType("SEPTA", 2);

            return stops;
        }
        

    }
}
