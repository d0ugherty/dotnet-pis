using DotNetPIS.App.Models;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<ActionResult> InfoBoard(int stopId = 4)
        {
            Stop stop = await _stopService.GetStopById(stopId);

            string stopName = await _septaRrService.GtfsNameToApiName(stop.Name);
            
            List<Arrival> arrivals = await GetTrainData(stopName);

            List<SelectListItem> stops = await _stopService.GetStopSelectList("SEPTA", 2);

            var viewModel = new InfoBoardViewModel
            {
                Title = $"Train Information for {stopName}",
                StationName = stopName,
                Arrivals = arrivals,
                Stops = stops
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public Task<ActionResult> UpdateBoard(int stopId)
        {
            return Task.FromResult<ActionResult>(RedirectToAction("InfoBoard", "InfoBoard", new { stopId }));
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
