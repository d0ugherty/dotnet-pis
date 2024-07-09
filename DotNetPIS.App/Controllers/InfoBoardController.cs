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
        private readonly InfoBoardService _infoBoardService;
        private readonly StopService _stopService;
        private readonly AlertService _alertService;

        public InfoBoardController(InfoBoardService infoBoardService, StopService stopService, AlertService alertService)
        {
            _infoBoardService = infoBoardService;
            _stopService = stopService;
            _alertService = alertService;
        }
        
        [HttpGet("InfoBoard")]
        public async Task<ActionResult> Index(string agency, int stopId)
        {
            InfoBoardViewModel viewModel = await RenderBoard(agency, stopId);
            
            return View(viewModel);
        }

        [HttpGet("InfoBoard/Update/{stopId}")]
        public async Task<ActionResult> UpdateBoard(string agencyName, int stopId)
        {
            InfoBoardViewModel viewModel = await RenderBoard(agencyName, stopId);
            
            return PartialView("InfoBoard/_Rows", viewModel);
        }

        private async Task<InfoBoardViewModel> RenderBoard(string agencyName, int stopId)
        {
            List<RouteAlert> routeAlerts = await _alertService.GetSeptaStopAlerts(stopId);
         
            Stop stop = await _stopService.GetStopById(stopId);
            
            List<SelectListItem> stops = await _stopService.GetStopSelectList(agencyName, RouteType.Rail);
            
            string stopName = _infoBoardService.GtfsNameToApiName(stop.Name);
        
            List<Arrival> arrivals = await _infoBoardService.GetRegionalRailArrivals(stopName, 5);
            
            var viewModel = new InfoBoardViewModel
            {
                Title = $"Train Information for {stopName}",
                AgencyName = agencyName,
                StationName = stopName,
                StopId = stopId,
                Arrivals = arrivals,
                SeptaAlerts = routeAlerts,
                Stops = stops
            };

            return viewModel;
        }
    }
}
