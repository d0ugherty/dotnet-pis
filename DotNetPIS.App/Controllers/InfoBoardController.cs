using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPIS.App.Controllers
{
    public class InfoBoardController : Controller
    {
        private readonly SeptaRegionalRailService _septaRrService;
        private readonly StopService _stopService;
        
        public InfoBoardController( SeptaRegionalRailService septaRrService, StopService stopService)
        {
            _septaRrService = septaRrService;
            _stopService = stopService;
        }

        public async Task<ActionResult> InfoBoard()
        {
            string stationName = "30th Street Station";
            
            List<Arrival> arrivals = await GetTrainData(stationName);

            List<Stop> stops = await _stopService.GetStopsByAgencyAndRouteType("SEPTA", 2);
            
            ViewData["Arrivals"] = arrivals;
            ViewData["Station Name"] = stationName;
            ViewData["Stops"] = stops;
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBoard(string stationName)
        {
            List<Arrival> arrivals = await GetTrainData(stationName);
            
            ViewData["Arrivals"] = arrivals;
            ViewData["Station Name"] = stationName;
            
            return PartialView("InfoBoard/_Rows", arrivals);
        }

        private async Task<List<Arrival>> GetTrainData(string stationName)
        {
            List<Arrival> arrivals = new List<Arrival>();

            var northbound = await _septaRrService.GetRegionalRailArrivals(stationName, "N", 5);
            var southbound = await _septaRrService.GetRegionalRailArrivals(stationName, "S", 5);
            
            arrivals.AddRange(northbound);
            arrivals.AddRange(southbound);

            return arrivals;
        }
    }
}
