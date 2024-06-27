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

        public InfoBoardController(InfoBoardService infoBoardService, StopService stopService)
        {
            _infoBoardService = infoBoardService;
            _stopService = stopService;
        }
        
        [HttpGet]
        public async Task<ActionResult> Index(int stopId = 14087)
        {
            InfoBoardViewModel viewModel = await RenderBoard(stopId);
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateBoard(int stopId)
        {
            InfoBoardViewModel viewModel = await RenderBoard(stopId);
            
            return PartialView("InfoBoard/_Rows", viewModel);
        }

        private async Task<InfoBoardViewModel> RenderBoard(int stopId)
        {
            var allArrivals = new List<Arrival>();
            
            Stop stop = await _stopService.GetStopById(stopId);
            
            List<SelectListItem> stops = await _stopService.GetStopSelectList("SEPTA", RouteType.Rail);

            string stopName = _infoBoardService.GtfsNameToApiName(stop.Name);
        
            List<Arrival> northboundArrivals = await _infoBoardService.GetRegionalRailArrivals(stopName, "N", 5);
            List<Arrival> southboundArrivals = await _infoBoardService.GetRegionalRailArrivals(stopName, "S", 5);
            
            allArrivals.AddRange(northboundArrivals);
            allArrivals.AddRange(southboundArrivals);
            
            var viewModel = new InfoBoardViewModel
            {
                Title = $"Train Information for {stopName}",
                StationName = stopName,
                StopId = stopId,
                Arrivals = allArrivals,
                Stops = stops
            };

            return viewModel;
        }
    }
}
