using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;

namespace ventaapp.Controllers
{
    public class FacturasController : Controller
    {
        private readonly VentasDbContext _context;

        public FacturasController(VentasDbContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index(DateTime? fechaDesde, DateTime? fechaHasta,
            int? idCliente, string tipoComprobante, string estado)
        {
            ViewData["FechaDesde"] = fechaDesde?.ToString("yyyy-MM-dd");
            ViewData["FechaHasta"] = fechaHasta?.ToString("yyyy-MM-dd");
            ViewData["IdCliente"] = idCliente;
            ViewData["TipoComprobante"] = tipoComprobante;
            ViewData["Estado"] = estado;

            var facturas = _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Producto)
                .Include(f => f.Venta)
                .AsQueryable();

            // Filtros
            if (fechaDesde.HasValue)
                facturas = facturas.Where(f => f.FechaEmision.Date >= fechaDesde.Value.Date);

            if (fechaHasta.HasValue)
                facturas = facturas.Where(f => f.FechaEmision.Date <= fechaHasta.Value.Date);

            if (idCliente.HasValue)
                facturas = facturas.Where(f => f.IdCliente == idCliente.Value);

            if (!string.IsNullOrEmpty(tipoComprobante))
                facturas = facturas.Where(f => f.TipoComprobanteFiscal == tipoComprobante);

            if (!string.IsNullOrEmpty(estado))
                facturas = facturas.Where(f => f.Estado == estado);

            var facturasList = await facturas.OrderByDescending(f => f.FechaEmision).ToListAsync();

            // Estadísticas
            var hoy = DateTime.Today;
            ViewBag.FacturasHoy = await _context.Facturas
                .Where(f => f.FechaEmision.Date == hoy && f.Estado == "Activa")
                .CountAsync();

            ViewBag.FacturasMes = await _context.Facturas
                .Where(f => f.FechaEmision.Month == hoy.Month &&
                           f.FechaEmision.Year == hoy.Year &&
                           f.Estado == "Activa")
                .CountAsync();

            ViewBag.MontoTotalMes = await _context.Facturas
                .Where(f => f.FechaEmision.Month == hoy.Month &&
                           f.FechaEmision.Year == hoy.Year &&
                           f.Estado == "Activa")
                .SumAsync(f => (decimal?)f.MontoTotal) ?? 0;

            ViewBag.ItbisMes = await _context.Facturas
                .Where(f => f.FechaEmision.Month == hoy.Month &&
                           f.FechaEmision.Year == hoy.Year &&
                           f.Estado == "Activa")
                .SumAsync(f => (decimal?)f.MontoItbis) ?? 0;

            // Clientes para filtro
            ViewBag.Clientes = await _context.Clientes.OrderBy(c => c.Nombres).ToListAsync();

            return View(facturasList);
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Producto)
                .Include(f => f.Venta)
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // POST: Facturas/Anular/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Anular(int id, string motivoAnulacion)
        {
            try
            {
                var factura = await _context.Facturas.FindAsync(id);

                if (factura == null)
                    return NotFound();

                if (factura.Estado == "Anulada")
                {
                    TempData["Error"] = "Esta factura ya está anulada.";
                    return RedirectToAction(nameof(Index));
                }

                factura.Estado = "Anulada";
                factura.MotivoAnulacion = motivoAnulacion;
                factura.FechaAnulacion = DateTime.Now;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Factura anulada exitosamente.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al anular la factura: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Facturas/Reporte607
        public async Task<IActionResult> Reporte607(int mes, int año)
        {
            if (mes == 0) mes = DateTime.Now.Month;
            if (año == 0) año = DateTime.Now.Year;

            var facturas = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Producto)
                .Where(f => f.FechaEmision.Month == mes &&
                           f.FechaEmision.Year == año &&
                           f.Estado == "Activa")
                .OrderBy(f => f.FechaEmision)
                .ToListAsync();

            ViewBag.Mes = mes;
            ViewBag.Año = año;
            ViewBag.TotalFacturas = facturas.Count;
            ViewBag.TotalVentas = facturas.Sum(f => f.MontoTotal - f.MontoItbis);
            ViewBag.TotalItbis = facturas.Sum(f => f.MontoItbis);
            ViewBag.TotalGeneral = facturas.Sum(f => f.MontoTotal);

            // Agrupar por tipo de comprobante
            ViewBag.PorTipo = facturas
                .GroupBy(f => f.TipoComprobanteFiscal)
                .Select(g => new
                {
                    Tipo = g.Key,
                    Cantidad = g.Count(),
                    MontoTotal = g.Sum(f => f.MontoTotal),
                    MontoItbis = g.Sum(f => f.MontoItbis)
                })
                .ToList();

            return View(facturas);
        }

        // GET: Facturas/GestionNCF
        public async Task<IActionResult> GestionNCF()
        {
            // Por ahora retorna vista vacía
            // En implementación completa, aquí se gestionarían las secuencias de NCF
            TempData["Info"] = "La gestión de secuencias NCF se implementará próximamente.";
            return View();
        }

        // GET: Facturas/DescargarPDF/5
        public async Task<IActionResult> DescargarPDF(int? id)
        {
            if (id == null)
                return NotFound();

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Producto)
                .Include(f => f.Venta)
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null)
                return NotFound();

            // Aquí se implementaría la generación del PDF
            // Por ahora redirige al detalle
            TempData["Info"] = "La generación de PDF se implementará próximamente. Puede usar la función de imprimir del navegador.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Facturas/EnviarCorreo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarCorreo(int id)
        {
            try
            {
                var factura = await _context.Facturas
                    .Include(f => f.Cliente)
                    .FirstOrDefaultAsync(m => m.IdFactura == id);

                if (factura == null)
                    return NotFound();

                // Aquí se implementaría el envío por correo
                // Por ahora solo muestra mensaje
                TempData["Info"] = $"El envío de facturas por correo se implementará próximamente. Se enviaría a: {factura.Cliente.CorreoElectronico}";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al enviar correo: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        // Método auxiliar para validar NCF
        private bool ValidarNCF(string ncf, string tipo)
        {
            // Validación básica del formato de NCF
            if (string.IsNullOrEmpty(ncf) || ncf.Length != 19)
                return false;

            // El NCF debe comenzar con el tipo (B01, B02, etc.)
            if (!ncf.StartsWith(tipo))
                return false;

            return true;
        }
    }
}
