using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPIS.App.Controllers
{
    public class InfoBoardController : Controller
    {
        private readonly SeptaRegionalRailService _septaRrService;
        
        public InfoBoardController( SeptaRegionalRailService septaRrService)
        {
            _septaRrService = septaRrService;
        }

        public async Task<ActionResult> InfoBoard()
        {
            string stationName = "30th Street Station";
            
            List<Arrival> arrivals = new List<Arrival>();

            var northbound = await _septaRrService.GetRegionalRailArrivals(stationName, "N", 5);
            var southbound = await _septaRrService.GetRegionalRailArrivals(stationName, "S", 5);
            
            arrivals.AddRange(northbound);
            arrivals.AddRange(southbound);

            ViewData["Arrivals"] = arrivals;
            ViewData["Station Name"] = stationName;
            
            return View();
        }
        
    }
}
