using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HSE_Solution7_EMailService.Models;

namespace HSE_Solution7_EMailService.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    //1. Сделать написание сообщений по Email, а не id
    //2. Сделать проверку уникальности Email (через dictionary?)
}
