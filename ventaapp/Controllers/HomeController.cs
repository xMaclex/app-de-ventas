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

        // Ventas los ultimos 7 dias
        var sevenDays = hoy.AddDays(-6);
        var ventasPorDia = await _context.Ventas
            .Where(v => v.FechaVenta.Date >= sevenDays && v.Estado == "Completada")
            .GroupBy(v => v.FechaVenta.Date)
            .Select(g => new {Fecha = g.Key, Total = g.Sum(v => v.Total)})
            .OrderBy(x => x.Fecha)
            .ToListAsync();

        var labels7Dias = new List<string>();
        var datos7Dias = new List<decimal>();

        for(int i = 6; i >= 0; i--)
        {
            var fecha = hoy.AddDays(-i);
            labels7Dias.Add(fecha.ToString("dd/MM"));
            var venta = ventasPorDia.FirstOrDefault(v => v.Fecha == fecha);
            datos7Dias.Add(venta?.Total ?? 0);
        }

        ViewBag.labels7Dias = System.Text.Json.JsonSerializer.Serialize(labels7Dias);
        ViewBag.datos7Dias = System.Text.Json.JsonSerializer.Serialize(datos7Dias);


        //Top 5 productos mas vendidos

        var topProductos = await _context.Facturas
        .Where(f => f.Estado == "Activa")
        .Include(f => f.Producto)
        .GroupBy(f => new {f.IdProducto, f.Producto.NombreProducto})
        .Select(g => new{Nombre = g.Key.NombreProducto, Cantidad = g.Count()})
        .OrderByDescending(x => x.Cantidad)
        .Take(5)
        .ToListAsync();

    ViewBag.topProductosNombres = System.Text.Json.JsonSerializer.Serialize(topProductos.Select(p => p.Nombre).ToList());
    ViewBag.topProductosCantidad = System.Text.Json.JsonSerializer.Serialize(topProductos.Select(p => p.Cantidad).ToList());

    //Top 5 clientes del mes

    var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
    var topClientes = await _context.Ventas
        .Where(v => v.FechaVenta >= inicioMes && v.Estado == "Completada")
        .Include(v => v.Cliente)
        .GroupBy(v => new {v.IdCliente, v.Cliente.Nombres, v.Cliente.Apellidos})
        .Select(g => new
        {
                Nombre = g.Key.Nombres + " " + g.Key.Apellidos,
            Total = g.Sum(v => v.Total)
        })
        .OrderByDescending(x => x.Total)
        .Take(5)
        .ToListAsync();

    ViewBag.topClientesNombre = System.Text.Json.JsonSerializer.Serialize(topClientes.Select(c => c.Nombre).ToList());
    ViewBag.topClientesTotales = System.Text.Json.JsonSerializer.Serialize(topClientes.Select(c => c.Total).ToList());


    //KPI mes
    ViewBag.TotalMes = await _context.Ventas
        .Where(v => v.FechaVenta >= inicioMes && v.Estado == "Completada")
        .SumAsync(v => (decimal?)v.Total) ?? 0;
    ViewBag.VentasMes = await _context.Ventas
        .Where(v => v.FechaVenta >= inicioMes && v.Estado == "Completada")
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
