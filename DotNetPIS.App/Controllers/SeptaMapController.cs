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
            
            MapViewModel viewModel = await RenderMap();
            
            return View(viewModel);
        }

        public async Task<MapViewModel> RenderMap()
        {
            (List<TrainView> trainData, List<Stop> trainStops) = await GetTrainData();

            List<Shape> shapeData = await GetShapeData(RouteType.Rail);
            
            var viewModel = new MapViewModel
            {
                SeptaTrainMarkers = trainData,
                Stops = trainStops,
                Shapes = shapeData
            };

            return viewModel;
        }

        private async Task<(List<TrainView>, List<Stop>)> GetTrainData()
        {
            List<TrainView> trainMarkers = await _septaRrService.GetTrainView();
            List<Stop> stops = await _stopService.GetStopsByAgencyAndRouteType("SEPTA", RouteType.Rail);
            
            return (trainMarkers, stops);
        }

        private async Task<List<Shape>> GetShapeData(RouteType routeType)
        {
            List<Route> routes = await _routeService.GetRoutesByAgencyAndType("SEPTA", routeType);

            List<Shape> shapes = new List<Shape>();
            
            foreach (var route in routes)
            {
                List<Shape> routeShapes = await _shapeService.GetShapesByRoute(route.Id);
                shapes.AddRange(routeShapes);
            }

            return shapes;
        }
        

        private async Task<List<TransitView>> GetTransitData()
        {
            var vehicles = new List<TransitView>();
            var routes = new List<Route>();

            List<Route> busRoutes = await _routeService.GetRoutesByAgencyAndType("SEPTA", RouteType.Bus);
            List<Route> trolleyRoutes = await _routeService.GetRoutesByAgencyAndType("SEPTA", RouteType.Tram);
            
            routes.AddRange(busRoutes);
            routes.AddRange(trolleyRoutes);
            
            foreach (var route in routes)
            {
                vehicles.AddRange(await _septaTransitService.GetTransitView(route.RouteNumber));
            }

            return vehicles;
        }

    }
}
