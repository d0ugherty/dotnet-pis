using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            var arrivals = await _septaRrService.GetRegionalRailArrivals("30th Street Station", "N", 10);

            foreach (var arrival in arrivals)
            {
                Console.WriteLine(arrival);
            }

            return Ok(new { Arrivals = arrivals });
        }
    }
}
