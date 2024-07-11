using DotNetPIS.App.Models;
using DotNetPIS.App.Models.Components;
using DotNetPIS.Domain.Models.GTFS;
using DotNetPIS.Domain.Models.SEPTA;
using DotNetPIS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetPIS.App.Controllers;

public class InfoBoardController : Controller
{
    private readonly InfoBoardService _infoBoardService;
    private readonly StopService _stopService;
    private readonly AlertService _alertService;
    private readonly string _controllerName;

    public InfoBoardController(InfoBoardService infoBoardService, StopService stopService,
        AlertService alertService)
    {
        _infoBoardService = infoBoardService;
        _stopService = stopService;
        _alertService = alertService;
        _controllerName = "InfoBoard";
    }

    [HttpGet("InfoBoard")]
    public async Task<ActionResult> Index(string agency, int stopId)
    {
        InfoBoardViewModel viewModel = await RenderBoard(agency, stopId);
        
        ViewData["Title"] = "Arrivals & Departures";
        
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

        var stopSelectViewModel = new StopSelectViewModel
        { 
            SelectController = _controllerName,
            SelectAction = "Index",
            AgencyName = agencyName,
            StopId = stopId,
            Stops = stops
        };
        
        var alertsViewModel = new AlertsViewModel
        {
            AgencyName = agencyName,
            SeptaAlerts = routeAlerts
        };
        
        var infoBoardViewModel = new InfoBoardViewModel
        {
            Title = $"Arrivals & Departures for {stopName}",
            AgencyName = agencyName,
            StationName = stopName,
            StopId = stopId,
            Arrivals = arrivals,
            StopSelectViewModel = stopSelectViewModel,
            AlertsViewModel = alertsViewModel
        };

        return infoBoardViewModel;
    }
}