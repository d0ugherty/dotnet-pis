using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPIS.App.Controllers
{
    public class MapController : Controller
    {

        private readonly StopService _stopService;

        public MapController(StopService stopService)
        {
            _stopService = stopService;
        }

        public ActionResult Index()
        {
            return View();
        }

        
        

        public ActionResult RenderMap()
        {
            throw new NotImplementedException();
        }

    }
}
