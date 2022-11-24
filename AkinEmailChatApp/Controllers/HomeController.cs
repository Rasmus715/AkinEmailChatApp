using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AkinEmailChatApp.Models;
using AkinEmailChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AkinEmailChatApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}