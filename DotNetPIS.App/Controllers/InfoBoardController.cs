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
            string direction = "N";
            
            List<Arrival> arrivals = await _septaRrService.GetRegionalRailArrivals(stationName, direction);

            ViewData["Arrivals"] = arrivals;
            ViewData["Station Name"] = stationName;
            
            return View();
        }
    }
}
