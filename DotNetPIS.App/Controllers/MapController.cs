using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPIS.App.Controllers
{
    public class MapController : Controller
    {

        private readonly MapService _mapService;

        public MapController(MapService mapService)
        {
            _mapService = mapService;
        }
        
        [HttpGet("Map")]
        public ActionResult Index()
        {
            return View();
        }
        
        public async Task<JsonResult> GetTrainData(string agencyName)
        {
            if (agencyName.Equals("SEPTA"))
            { 
                List<TrainView> trainData  = await _mapService.GetTrainView();
                
                return Json(trainData);
            }
            
            throw new InvalidOperationException($"Invalid agency name {agencyName}.");
        }
        
        public async Task<JsonResult> GetShapeData(RouteType routeType, string agencyName)
        {
            Dictionary<string, List<Shape>> shapeData = await _mapService.GetShapeData(routeType, agencyName);

            return Json(shapeData);
        }

        public async Task<JsonResult> GetStops(RouteType routeType, string agencyName)
        {
            List<Stop> stops = await _mapService.GetStopsByAgencyAndRouteType(agencyName, routeType);

            return Json(stops);
        }
        
    }
}
