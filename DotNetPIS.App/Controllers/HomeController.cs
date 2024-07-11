using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNetPIS.App.Models;
using DotNetPIS.Domain.Services;

namespace DotNetPIS.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HomeService _homeService;
    
    public HomeController(ILogger<HomeController> logger, HomeService homeService)
    {
        _logger = logger;
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            InfoSources = _homeService.GetInfoSources()
        };
        
        return View(viewModel);
    }

    public IActionResult AgencyHome(int sourceId, HomeViewModel viewModel)
    {
        viewModel.SelectedSource = _homeService.GetSourceById(sourceId);
        
        return View("AgencyHome", viewModel);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
