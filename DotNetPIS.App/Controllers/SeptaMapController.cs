using DotNetPIS.App.Models;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Route = DotNetPIS.Domain.Models.GTFS.Route;

namespace DotNetPIS.App.Controllers
{
    public class MapController : Controller
    {

        private readonly StopService _stopService;
        private readonly MapService _mapService;
        private readonly SeptaRegionalRailService _septaRrService;
        private readonly SeptaTransitService _septaTransitService;

        public MapController(StopService stopService, SeptaRegionalRailService septaRrService, 
            SeptaTransitService septaTransitService, MapService mapService)
        {
            _stopService = stopService;
            _septaRrService = septaRrService;
            _septaTransitService = septaTransitService;
            _mapService = mapService;
        }
        
        [HttpGet("Map")]
        public async Task<ActionResult> Index()
        {
            return View();
        }
        
        public async Task<JsonResult> GetTrainData()
        {
            List<TrainView> trainData = await _septaRrService.GetTrainView();

            return Json(trainData);
        }
        
        public async Task<JsonResult> GetShapeData(RouteType routeType, string agencyName)
        {
            List<Shape> shapeData = await _mapService.GetShapeData(routeType, agencyName);

            return Json(shapeData);
        }
        
    }
}
