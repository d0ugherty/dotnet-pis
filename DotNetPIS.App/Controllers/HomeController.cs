using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNetPIS.App.Models;
using DotNetPIS.Domain.Services;

namespace DotNetPIS.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AlertService _alertService;

    public HomeController(ILogger<HomeController> logger, AlertService alertService)
    {
        _logger = logger;
        _alertService = alertService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            SeptaAlerts = await _alertService.GetAllSeptaAlerts()
        };
        
        return View(viewModel);
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
