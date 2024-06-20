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

        public InfoBoardController(SeptaRegionalRailService septaRrService, StopService stopService)
        {
            _septaRrService = septaRrService;
            _stopService = stopService;
        }
        
        [HttpGet]
        public async Task<ActionResult> Index(int stopId = 4)
        {
            InfoBoardViewModel viewModel = await RenderBoard(stopId);
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateBoard(int stopId)
        {
            var viewModel = await RenderBoard(stopId);
            
            //return RedirectToAction("Index", new {viewModel.StopId});
            return PartialView("InfoBoard/_Rows", viewModel);
            //return View(viewModel);
        }

        private async Task<List<Arrival>> GetTrainData(string stationName)
        {
            List<Arrival> arrivals = new List<Arrival>();

            List<Arrival> northbound = await _septaRrService.GetRegionalRailArrivals(stationName, "N", 5);
            List<Arrival> southbound = await _septaRrService.GetRegionalRailArrivals(stationName, "S", 5);

            arrivals.AddRange(northbound);
            arrivals.AddRange(southbound);

            return arrivals;
        }

        private async Task<InfoBoardViewModel> RenderBoard(int stopId)
        {
            Stop stop = await _stopService.GetStopById(stopId);

            string stopName = _septaRrService.GtfsNameToApiName(stop.Name);

            List<Arrival> arrivals = await GetTrainData(stopName);

            List<SelectListItem> stops = await _stopService.GetStopSelectList("SEPTA", 2);

            var viewModel = new InfoBoardViewModel
            {
                Title = $"Train Information for {stopName}",
                StationName = stopName,
                StopId = stopId,
                Arrivals = arrivals,
                Stops = stops
            };

            return viewModel;
        }
    }
}
