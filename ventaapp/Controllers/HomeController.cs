using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Models;

namespace ventaapp.Controllers;

[Microsoft.AspNetCore.Authorization.Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ventaapp.Data.VentasDbContext _context;

    public HomeController(ILogger<HomeController> logger, ventaapp.Data.VentasDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Estadísticas básicas para el dashboard
        var hoy = DateTime.Today;
        ViewBag.VentasHoy = await _context.Ventas
            .Where(v => v.FechaVenta.Date == hoy && v.Estado == "Completada")
            .CountAsync();
        ViewBag.TotalHoy = await _context.Ventas
            .Where(v => v.FechaVenta.Date == hoy && v.Estado == "Completada")
            .SumAsync(v => (decimal?)v.Total) ?? 0;

        ViewBag.ProductosStockBajo = await _context.Productos
            .Where(p => p.Stock < 10 && p.Estado == "Activo")
            .CountAsync();

        return View();
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
