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
        private readonly ShapeService _shapeService;
        private readonly RouteService _routeService;
        private readonly SeptaRegionalRailService _septaRrService;
        private readonly SeptaTransitService _septaTransitService;

        public MapController(StopService stopService, ShapeService shapeService, SeptaRegionalRailService septaRrService, 
            SeptaTransitService septaTransitService, RouteService routeService)
        {
            _stopService = stopService;
            _shapeService = shapeService;
            _septaRrService = septaRrService;
            _septaTransitService = septaTransitService;
            _routeService = routeService;
        }
        
        [HttpGet("Map")]
        public async Task<ActionResult> Index()
        {
            bool getTrainData = true;
            
            MapViewModel viewModel = await RenderMap(getTrainData);
            
            return View(viewModel);
        }

        public async Task<MapViewModel> RenderMap(bool getTrainData = true)
        {
            List<TrainView> trainData = new List<TrainView>();
            List<Stop> trainStops = new List<Stop>();
            
            if (getTrainData)
            {
                (trainData, trainStops) = await GetTrainData();
            }
            
            var viewModel = new MapViewModel
            {
                SeptaTrainMarkers = trainData,
                Stops = trainStops
            };

            return viewModel;
        }

        private async Task<(List<TrainView>, List<Stop>)> GetTrainData()
        {
            List<TrainView> trainMarkers = await _septaRrService.GetTrainView();
            List<Stop> stops = await _stopService.GetStopsByAgencyAndRouteType("SEPTA", 2);
            
            return (trainMarkers, stops);
        }
        

        private async Task<List<TransitView>> GetTransitData(List<Route> routes)
        {
            var vehicles = new List<TransitView>();
            
            foreach (var route in routes)
            {
                vehicles.AddRange(await _septaTransitService.GetTransitView(route.RouteNumber));
            }

            return vehicles;
        }

    }
}
