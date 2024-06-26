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
        private readonly RouteService _routeService;
        private readonly SeptaRegionalRailService _septaRrService;
        private readonly SeptaTransitService _septaTransitService;

        public MapController(StopService stopService, SeptaRegionalRailService septaRrService, 
            SeptaTransitService septaTransitService, RouteService routeService, MapService mapService)
        {
            _stopService = stopService;
            _septaRrService = septaRrService;
            _septaTransitService = septaTransitService;
            _routeService = routeService;
            _mapService = mapService;
        }
        
        [HttpGet("Map")]
        public async Task<ActionResult> Index()
        {
            bool getTrainData = true;
            
            MapViewModel viewModel = await RenderMap();
            
            return View(viewModel);
        }

        public async Task<MapViewModel> RenderMap()
        {
            List<TrainView> trainData = await _septaRrService.GetTrainView();
            
            List<Stop> stops = await _stopService.GetStopsByAgencyAndRouteType("SEPTA", RouteType.Rail);

            List<Shape> shapeData = await _mapService.GetShapeData(RouteType.Rail, "SEPTA");
            
            var viewModel = new MapViewModel
            {
                SeptaTrainMarkers = trainData,
                Stops = stops,
                Shapes = shapeData
            };

            return viewModel;
        }
        
    }
}
