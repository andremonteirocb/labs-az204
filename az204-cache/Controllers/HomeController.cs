using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using az204_cache.Models;

namespace az204_cache.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRedisService _redisService;
    public HomeController(ILogger<HomeController> logger,
        IRedisService redisService)
    {
        _logger = logger;
        _redisService = redisService;
    }

    public async Task<IActionResult> Index(string chave = "favorite:flavor", string valor = "teste")
    {
        await _redisService.Set(chave, valor);
        var value = await _redisService.Get(chave);
        await _redisService.Delete(chave);
        return View("Index", value);
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
