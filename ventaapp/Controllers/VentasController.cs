using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;
using System.Text.Json;
using System.Security.Claims;

namespace ventaapp.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
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
            ViewData["IdCliente"]  = idCliente;
            ViewData["MetodoPago"] = metodoPago;
            ViewData["Estado"]     = estado;

            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .AsQueryable();

            if (fechaDesde.HasValue)
                ventas = ventas.Where(v => v.FechaVenta.Date >= fechaDesde.Value.Date);
            if (fechaHasta.HasValue)
                ventas = ventas.Where(v => v.FechaVenta.Date <= fechaHasta.Value.Date);
            if (idCliente.HasValue)
                ventas = ventas.Where(v => v.IdCliente == idCliente.Value);
            if (!string.IsNullOrEmpty(metodoPago))
                ventas = ventas.Where(v => v.MetodoPago == metodoPago);
            if (!string.IsNullOrEmpty(estado))
                ventas = ventas.Where(v => v.Estado == estado);

            var ventasList = await ventas.OrderByDescending(v => v.FechaVenta).ToListAsync();

            var hoy = DateTime.Today;
            ViewBag.VentasHoy  = await _context.Ventas.Where(v => v.FechaVenta.Date == hoy && v.Estado == "Completada").CountAsync();
            ViewBag.TotalHoy   = await _context.Ventas.Where(v => v.FechaVenta.Date == hoy && v.Estado == "Completada").SumAsync(v => (decimal?)v.Total) ?? 0;
            ViewBag.VentasMes  = await _context.Ventas.Where(v => v.FechaVenta.Month == hoy.Month && v.FechaVenta.Year == hoy.Year && v.Estado == "Completada").CountAsync();
            ViewBag.TotalMes   = await _context.Ventas.Where(v => v.FechaVenta.Month == hoy.Month && v.FechaVenta.Year == hoy.Year && v.Estado == "Completada").SumAsync(v => (decimal?)v.Total) ?? 0;
            ViewBag.Clientes   = await _context.Clientes.OrderBy(c => c.Nombres).ToListAsync();

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
        public async Task<IActionResult> ProcesarVenta([FromForm] string? carritoJson, [FromForm] Venta venta)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // ── 1. Validaciones básicas ────────────────────────────────────
                if (string.IsNullOrEmpty(carritoJson))
                {
                    TempData["Error"] = "El carrito está vacío.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                var carrito = JsonSerializer.Deserialize<List<VentaDetalle>>(carritoJson);
                if (carrito == null || !carrito.Any())
                {
                    TempData["Error"] = "El carrito está vacío.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                if (venta.IdCliente <= 0)
                {
                    TempData["Error"] = "Debe seleccionar un cliente.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                var cliente = await _context.Clientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.IdCliente == venta.IdCliente);

                if (cliente == null)
                {
                    TempData["Error"] = "El cliente seleccionado no existe.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                if (string.IsNullOrEmpty(venta.MetodoPago))
                {
                    TempData["Error"] = "Debe seleccionar un método de pago.";
                    return RedirectToAction(nameof(PuntoVenta));
                }

                // ── 2. Validar stock ───────────────────────────────────────────
                var idsProductos = carrito.Select(i => i.IdProducto).ToList();
                var productosDb  = await _context.Productos
                    .Where(p => idsProductos.Contains(p.IdProducto))
                    .ToDictionaryAsync(p => p.IdProducto);

                var productosInsuficientes = new List<string>();
                foreach (var item in carrito)
                {
                    if (!productosDb.TryGetValue(item.IdProducto, out var prod))
                    {
                        TempData["Error"] = $"El producto '{item.NombreProducto}' no existe.";
                        await transaction.RollbackAsync();
                        return RedirectToAction(nameof(PuntoVenta));
                    }
                    if (prod.Stock < item.Cantidad)
                        productosInsuficientes.Add($"{prod.NombreProducto} — disponible: {prod.Stock}, solicitado: {item.Cantidad}");
                }

                if (productosInsuficientes.Any())
                {
                    TempData["Error"] = "Stock insuficiente:\n" + string.Join("\n", productosInsuficientes);
                    await transaction.RollbackAsync();
                    return RedirectToAction(nameof(PuntoVenta));
                }

                // ── 3. Calcular totales ────────────────────────────────────────
                decimal subtotal = carrito.Sum(d => d.Subtotal);
                decimal itbis    = carrito.Sum(d => d.MontoImpuesto);
                decimal descuento = 0;

                if (venta.TipoDescuento == "Porcentaje" && venta.Descuento > 0)
                    descuento = subtotal * (venta.Descuento / 100);
                else if (venta.Descuento > 0)
                    descuento = venta.Descuento;

                decimal total = subtotal + itbis - descuento;

                if (total < 0)
                {
                    TempData["Error"] = "El total de la venta no puede ser negativo.";
                    await transaction.RollbackAsync();
                    return RedirectToAction(nameof(PuntoVenta));
                }

                // ── 4. Usuario autenticado ─────────────────────────────────────
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    TempData["Error"] = "No se pudo identificar al usuario autenticado.";
                    await transaction.RollbackAsync();
                    return RedirectToAction(nameof(PuntoVenta));
                }

                // ── 5. Obtener y validar la secuencia NCF ─────────────────────
// ── 5. Obtener y validar la secuencia NCF ─────────────────────

                string tipoNcf      = venta.TipoComprobante ?? "Preforma";
                string? ncfGenerado = null;
                SecuenciaNcf? secuenciaNcf = null;

                if (venta.TipoComprobante != "Preforma")
                {
                    // Determinar tipo según TipoDocumento del cliente
                    tipoNcf = (cliente.TipoDocumento == "RNC") ? "B01" : venta.TipoComprobante!;

                    // Bloquear la fila para evitar concurrencia (PESSIMISTIC LOCK)
                    var ahora = DateTime.Today;

                    secuenciaNcf = await _context.SecuenciaNcf
                        .Where(s => s.TipoComprobante == tipoNcf
                                && s.Activa
                                && s.FechaVencimiento >= ahora       // reemplaza !s.Vencida
                                && s.NumeroActual <= s.NumeroFinal)
                        .FirstOrDefaultAsync();

                    if (secuenciaNcf == null)
                    {
                        TempData["Error"] = $"No hay secuencia NCF activa para el tipo {tipoNcf}. " +
                                            "Configure una en Configuración → Comprobantes Fiscales (NCF).";
                        await transaction.RollbackAsync();
                        return RedirectToAction(nameof(PuntoVenta));
                    }

                    // Generar el NCF e incrementar el contador
                    ncfGenerado = secuenciaNcf.GenerarProximoNcf();
                    if (ncfGenerado == null)
                    {
                        TempData["Error"] = "La secuencia NCF está agotada o vencida. Configure una nueva.";
                        await transaction.RollbackAsync();
                        return RedirectToAction(nameof(PuntoVenta));
                    }
                }

                // ── 6. Crear y guardar la venta ───────────────────────────────
                var nuevaVenta = new Venta
                {
                    FechaVenta      = DateTime.Now,
                    IdCliente       = venta.IdCliente,
                    Subtotal        = subtotal,
                    Itbis           = itbis,
                    Descuento       = descuento,
                    TipoDescuento   = venta.TipoDescuento ?? "Monto",
                    Total           = total,
                    MetodoPago      = venta.MetodoPago,
                    TipoComprobante = tipoNcf,
                    NcfGenerado     = ncfGenerado,                        // null si Preforma
                    IdSecuenciaNcf  = secuenciaNcf?.IdSecuencia ?? null,     // 0 si Preforma
                    TipoVenta       = venta.TipoVenta ?? "Contado",
                    Notas           = venta.Notas ?? string.Empty,
                    Estado          = "Completada",
                    IdUsuario       = userId
                };

                _context.Ventas.Add(nuevaVenta);
                // Guardar venta y actualizar NumeroActual de la secuencia en un solo hit
                await _context.SaveChangesAsync();

                // ── 7. Descontar stock y crear facturas ───────────────────────
                foreach (var item in carrito)
                {
                    var prod = productosDb[item.IdProducto];
                    prod.Stock -= item.Cantidad;
                    _context.Entry(prod).Property(p => p.Stock).IsModified = true;

                    decimal precioUnit = item.PrecioUnitario;
                    decimal itbisUnit  = precioUnit * (item.Impuesto / 100);

                    for (int i = 0; i < item.Cantidad; i++)
                    {
                        var factura = new Factura
                        {
                            IdVenta               = nuevaVenta.IdVenta,
                            IdCliente             = venta.IdCliente,
                            IdProducto            = item.IdProducto,
                            NumeroFactura         = $"F-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                            FechaEmision          = DateTime.Now,
                            RncEmpresa            = "000-00000-0",
                            NombreEmpresa         = "VentaApp",
                            DireccionEmpresa      = "Santo Domingo, RD",
                            Ncf                   = ncfGenerado ?? string.Empty,       // ← null-safe
                            TipoComprobanteFiscal = tipoNcf,
                            IdSecuenciaNcf        = secuenciaNcf?.IdSecuencia ?? null, // ← null-safe
                            Estado                = "Activa",
                            IdUsuario             = userId,
                            MontoTotal            = precioUnit + itbisUnit,
                            MontoItbis            = itbisUnit,
                            MotivoAnulacion       = string.Empty
                        };

                        _context.Facturas.Add(factura);
                    }
                }

                // ── 8. Guardar todo y confirmar transacción ───────────────────
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["Success"] = $"Venta #{nuevaVenta.IdVenta} procesada exitosamente. NCF: {ncfGenerado}";
                return RedirectToAction(nameof(Details), new { id = nuevaVenta.IdVenta });
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                TempData["Error"] = $"Error de base de datos: {dbEx.InnerException?.Message ?? dbEx.Message}";
                return RedirectToAction(nameof(PuntoVenta));
            }
            catch (JsonException jsonEx)
            {
                await transaction.RollbackAsync();
                TempData["Error"] = $"Error al procesar el carrito: {jsonEx.Message}";
                return RedirectToAction(nameof(PuntoVenta));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["Error"] = $"Error inesperado: {ex.Message}";
                return RedirectToAction(nameof(PuntoVenta));
            }
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Facturas)
                    .ThenInclude(f => f.Producto)
                .FirstOrDefaultAsync(m => m.IdVenta == id);

            if (venta == null) return NotFound();
            return View(venta);
        }

        // POST: Ventas/Anular/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Anular(int id, string motivoAnulacion)
        {
            try
            {
                var venta = await _context.Ventas
                    .Include(v => v.Facturas)
                    .FirstOrDefaultAsync(v => v.IdVenta == id);

                if (venta == null) return NotFound();

                if (venta.Estado == "Anulada")
                {
                    TempData["Error"] = "Esta venta ya está anulada.";
                    return RedirectToAction(nameof(Index));
                }

                venta.Estado = "Anulada";
                venta.Notas += $" | ANULADA: {motivoAnulacion} - {DateTime.Now:dd/MM/yyyy HH:mm}";

                foreach (var factura in venta.Facturas)
                {
                    factura.Estado = "Anulada";
                    var producto = await _context.Productos.FindAsync(factura.IdProducto);
                    if (producto != null) producto.Stock += 1;
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
                case "semana": fechaInicio = DateTime.Today.AddDays(-7); break;
                case "mes":    fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); break;
                default:       fechaInicio = DateTime.Today; break;
            }

            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin && v.Estado == "Completada")
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();

            ViewBag.Periodo      = periodo;
            ViewBag.FechaInicio  = fechaInicio;
            ViewBag.FechaFin     = fechaFin;
            ViewBag.TotalVentas  = ventas.Count;
            ViewBag.MontoTotal   = ventas.Sum(v => v.Total);
            ViewBag.PromedioVenta = ventas.Any() ? ventas.Average(v => v.Total) : 0;
            ViewBag.VentasPorMetodo = ventas
                .GroupBy(v => v.MetodoPago)
                .Select(g => new { Metodo = g.Key, Cantidad = g.Count(), Total = g.Sum(v => v.Total) })
                .ToList();
            ViewBag.TopClientes = ventas
                .GroupBy(v => v.Cliente)
                .Select(g => new { Cliente = g.Key?.NombreCompleto, Cantidad = g.Count(), Total = g.Sum(v => v.Total) })
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
                .Where(v => v.FechaVenta.Date == fechaCierre.Date && v.Estado == "Completada")
                .OrderBy(v => v.FechaVenta)
                .ToListAsync();

            ViewBag.FechaCierre         = fechaCierre;
            ViewBag.TotalVentas         = ventas.Count;
            ViewBag.MontoTotal          = ventas.Sum(v => v.Total);
            ViewBag.TotalEfectivo       = ventas.Where(v => v.MetodoPago == "Efectivo").Sum(v => v.Total);
            ViewBag.TotalTarjeta        = ventas.Where(v => v.MetodoPago == "Tarjeta").Sum(v => v.Total);
            ViewBag.TotalTransferencia  = ventas.Where(v => v.MetodoPago == "Transferencia").Sum(v => v.Total);

            return View(ventas);
        }

        // API: Buscar producto
        [HttpGet]
        public async Task<JsonResult> BuscarProducto(string termino)
        {
            if (string.IsNullOrEmpty(termino) || termino.Length < 2)
                return Json(new List<object>());

            var productos = await _context.Productos
                .Where(p => p.Estado == "Activo" &&
                           (p.NombreProducto.Contains(termino) || p.CodigoProducto.Contains(termino)))
                .Take(10)
                .Select(p => new
                {
                    p.IdProducto,
                    p.CodigoProducto,
                    p.NombreProducto,
                    p.PrecioVenta,
                    p.Impuesto,
                    p.Stock
                })
                .ToListAsync();

            return Json(productos);
        }
    }
}