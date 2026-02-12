using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;
using System.Text.Json;

namespace ventaapp.Controllers
{
    public class VentasController : Controller
    {
        private readonly VentasDbContext _context;

        public VentasController(VentasDbContext context)
        {
            _context = context;
        }

        // GET: Ventas (Historial)
        public async Task<IActionResult> Index(DateTime? fechaDesde, DateTime? fechaHasta, 
            int? idCliente, string metodoPago, string estado)
        {
            ViewData["FechaDesde"] = fechaDesde?.ToString("yyyy-MM-dd");
            ViewData["FechaHasta"] = fechaHasta?.ToString("yyyy-MM-dd");
            ViewData["IdCliente"] = idCliente;
            ViewData["MetodoPago"] = metodoPago;
            ViewData["Estado"] = estado;

            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .AsQueryable();

            // Filtros
            if (fechaDesde.HasValue)
                ventas = ventas.Where(v => v.Fechaventa.Date >= fechaDesde.Value.Date);

            if (fechaHasta.HasValue)
                ventas = ventas.Where(v => v.Fechaventa.Date <= fechaHasta.Value.Date);

            if (idCliente.HasValue)
                ventas = ventas.Where(v => v.IdCliente == idCliente.Value);

            if (!string.IsNullOrEmpty(metodoPago))
                ventas = ventas.Where(v => v.MetodoPago == metodoPago);

            if (!string.IsNullOrEmpty(estado))
                ventas = ventas.Where(v => v.Estado == estado);

            var ventasList = await ventas.OrderByDescending(v => v.Fechaventa).ToListAsync();

            // Estadísticas
            var hoy = DateTime.Today;
            ViewBag.VentasHoy = await _context.Ventas
                .Where(v => v.Fechaventa.Date == hoy && v.Estado == "Completada")
                .CountAsync();
            
            ViewBag.TotalHoy = await _context.Ventas
                .Where(v => v.Fechaventa.Date == hoy && v.Estado == "Completada")
                .SumAsync(v => (decimal?)v.Total) ?? 0;

            ViewBag.VentasMes = await _context.Ventas
                .Where(v => v.Fechaventa.Month == hoy.Month && 
                           v.Fechaventa.Year == hoy.Year && 
                           v.Estado == "Completada")
                .CountAsync();

            ViewBag.TotalMes = await _context.Ventas
                .Where(v => v.Fechaventa.Month == hoy.Month && 
                           v.Fechaventa.Year == hoy.Year && 
                           v.Estado == "Completada")
                .SumAsync(v => (decimal?)v.Total) ?? 0;

            // Lista de clientes para filtro
            ViewBag.Clientes = await _context.Clientes.OrderBy(c => c.Nombres).ToListAsync();

            return View(ventasList);
        }

        // GET: Ventas/PuntoVenta
        public async Task<IActionResult> PuntoVenta()
        {
            var viewModel = new PuntoVentaViewModel
            {
                Clientes = await _context.Clientes.OrderBy(c => c.Nombres).ToListAsync(),
                Productos = await _context.Productos
                    .Where(p => p.Estado == "Activo")
                    .OrderBy(p => p.NombreProducto)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Ventas/ProcesarVenta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarVenta([FromForm] string carritoJson, [FromForm] Venta venta)
        {
            try
            {
                // Deserializar el carrito
                var carrito = JsonSerializer.Deserialize<List<VentaDetalle>>(carritoJson);

                if (carrito == null || !carrito.Any())
                {
                    TempData["Error"] = "El carrito está vacío.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                // Calcular totales
                decimal subtotal = carrito.Sum(d => d.Subtotal);
                decimal itbis = carrito.Sum(d => d.MontoImpuesto);
                decimal descuento = 0;

                // Calcular descuento
                if (venta.TipoDescuento == "Porcentaje")
                    descuento = subtotal * (venta.Descuento / 100);
                else
                    descuento = venta.Descuento;

                decimal total = subtotal + itbis - descuento; 

                // Crear la venta
                var nuevaVenta = new Venta
                {
                    Fechaventa = DateTime.Now,
                    IdCliente = venta.IdCliente,
                    Subtotal = subtotal,
                    Itbis = itbis,
                    Descuento = descuento,
                    TipoDescuento = venta.TipoDescuento,
                    Total = total,
                    MetodoPago = venta.MetodoPago,
                    TipoComprobante = venta.TipoComprobante,
                    NumeroComprobante = GenerarNumeroComprobante(),
                    TipoVenta = venta.TipoVenta,
                    Notas = venta.Notas ?? string.Empty,
                    Estado = "Completada",
                    IdUsuario = venta.IdUsuario // Aquí puedes poner el usuario autenticado
                };

                _context.Ventas.Add(nuevaVenta);
                await _context.SaveChangesAsync();

                // Crear las facturas (una por cada producto)
                foreach (var item in carrito)
                {
                    var factura = new Factura
                    {
                        IdVenta = nuevaVenta.IdVenta,
                        IdCliente = venta.IdCliente,
                        IdProducto = item.IdProducto,
                        NumeroFactura = $"F-{nuevaVenta.IdVenta:D6}-{item.IdProducto:D4}",
                        FechaEmision = DateTime.Now,
                        RncEmpresa = "000-00000-0", // Configurar según tu empresa
                        NombreEmpresa = "VentaApp", // Configurar según tu empresa
                        DireccionEmpresa = "Santo Domingo, RD", // Configurar según tu empresa
                        Ncf = GenerarNCF(),
                        TipoComprobanteFiscal = venta.TipoComprobante,
                        Estado = "Activa"
                    };

                    _context.Facturas.Add(factura);
                }

                await _context.SaveChangesAsync();

                TempData["Success"] = $"Venta #{nuevaVenta.IdVenta} procesada exitosamente.";
                return RedirectToAction(nameof(Details), new { id = nuevaVenta.IdVenta });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al procesar la venta: {ex.Message}";
                return RedirectToAction(nameof(PuntoVenta));
            }
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Facturas)
                    .ThenInclude(f => f.Producto)
                .FirstOrDefaultAsync(m => m.IdVenta == id);

            if (venta == null)
                return NotFound();

            return View(venta);
        }

        // POST: Ventas/Anular/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Anular(int id, string motivoAnulacion)
        {
            try
            {
                var venta = await _context.Ventas.FindAsync(id);

                if (venta == null)
                    return NotFound();

                if (venta.Estado == "Anulada")
                {
                    TempData["Error"] = "Esta venta ya está anulada.";
                    return RedirectToAction(nameof(Index));
                }

                venta.Estado = "Anulada";
                venta.Notas += $" | ANULADA: {motivoAnulacion} - {DateTime.Now:dd/MM/yyyy HH:mm}";

                // Anular facturas asociadas
                var facturas = await _context.Facturas.Where(f => f.IdVenta == id).ToListAsync();
                foreach (var factura in facturas)
                {
                    factura.Estado = "Anulada";
                }

                await _context.SaveChangesAsync();

                TempData["Success"] = "Venta anulada exitosamente.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al anular la venta: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Ventas/Reportes
        public async Task<IActionResult> Reportes(string periodo = "hoy")
        {
            DateTime fechaInicio;
            DateTime fechaFin = DateTime.Now;

            switch (periodo.ToLower())
            {
                case "hoy":
                    fechaInicio = DateTime.Today;
                    break;
                case "semana":
                    fechaInicio = DateTime.Today.AddDays(-7);
                    break;
                case "mes":
                    fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    break;
                default:
                    fechaInicio = DateTime.Today;
                    break;
            }

            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Where(v => v.Fechaventa >= fechaInicio && 
                           v.Fechaventa <= fechaFin && 
                           v.Estado == "Completada")
                .OrderByDescending(v => v.Fechaventa)
                .ToListAsync();

            // Estadísticas
            ViewBag.Periodo = periodo;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.TotalVentas = ventas.Count;
            ViewBag.MontoTotal = ventas.Sum(v => v.Total);
            ViewBag.PromedioVenta = ventas.Any() ? ventas.Average(v => v.Total) : 0;
            
            // Ventas por método de pago
            ViewBag.VentasPorMetodo = ventas
                .GroupBy(v => v.MetodoPago)
                .Select(g => new { Metodo = g.Key, Cantidad = g.Count(), Total = g.Sum(v => v.Total) })
                .ToList();

            // Top 5 clientes
            ViewBag.TopClientes = ventas
                .GroupBy(v => v.Cliente)
                .Select(g => new { 
                    Cliente = g.Key.NombreCompleto, 
                    Cantidad = g.Count(), 
                    Total = g.Sum(v => v.Total) 
                })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToList();

            return View(ventas);
        }

        // GET: Ventas/CierreCaja
        public async Task<IActionResult> CierreCaja(DateTime? fecha)
        {
            var fechaCierre = fecha ?? DateTime.Today;
            
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Where(v => v.Fechaventa.Date == fechaCierre.Date && v.Estado == "Completada")
                .OrderBy(v => v.Fechaventa)
                .ToListAsync();

            ViewBag.FechaCierre = fechaCierre;
            ViewBag.TotalVentas = ventas.Count;
            ViewBag.MontoTotal = ventas.Sum(v => v.Total);
            ViewBag.TotalEfectivo = ventas.Where(v => v.MetodoPago == "Efectivo").Sum(v => v.Total);
            ViewBag.TotalTarjeta = ventas.Where(v => v.MetodoPago == "Tarjeta").Sum(v => v.Total);
            ViewBag.TotalTransferencia = ventas.Where(v => v.MetodoPago == "Transferencia").Sum(v => v.Total);

            return View(ventas);
        }

        // Método auxiliar para generar número de comprobante
        private string GenerarNumeroComprobante()
        {
            var ultimaVenta = _context.Ventas.OrderByDescending(v => v.IdVenta).FirstOrDefault();
            var numero = (ultimaVenta?.IdVenta ?? 0) + 1;
            return $"V-{DateTime.Now:yyyyMMdd}-{numero:D6}";
        }

        // Método auxiliar para generar NCF (simulado)
        private string GenerarNCF()
        {
            // En producción, esto debería conectarse con el sistema de la DGII
            var random = new Random();
            return $"B01{DateTime.Now:yyyyMMdd}{random.Next(10000000, 99999999)}";
        }

        // API: Buscar producto
        [HttpGet]
        public async Task<JsonResult> BuscarProducto(string termino)
        {
            var productos = await _context.Productos
                .Where(p => p.Estado == "Activo" && 
                           (p.NombreProducto.Contains(termino) || 
                            p.CodigoProducto.Contains(termino)))
                .Take(10)
                .Select(p => new
                {
                    p.IdProducto,
                    p.CodigoProducto,
                    p.NombreProducto,
                    p.PrecioVenta,
                    p.Impuesto
                })
                .ToListAsync();

            return Json(productos);
        }
    }
}