using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;using OfficeOpenXml;
namespace ventaapp.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ClientesController : Controller
    {
        private readonly VentasDbContext _context;

        public ClientesController(VentasDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string searchString, string tipoDocumento)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["TipoDocumentoFilter"] = tipoDocumento;

            var clientes = from c in _context.Clientes
                          select c;

            // Filtro por búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(c => 
                    c.Nombres.Contains(searchString) ||
                    c.Apellidos.Contains(searchString) ||
                    c.NumeroDocumento.Contains(searchString) ||
                    c.CorreoElectronico.Contains(searchString));
            }

            // Filtro por tipo de documento
            if (!string.IsNullOrEmpty(tipoDocumento))
            {
                clientes = clientes.Where(c => c.TipoDocumento == tipoDocumento);
            }

            var clientesList = await clientes.OrderBy(c => c.Nombres).ToListAsync();

            // Estadísticas para el dashboard
            ViewBag.TotalClientes = await _context.Clientes.CountAsync();
            ViewBag.ClientesActivos = clientesList.Count;

            return View(clientesList);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Ventas)
                .Include(c => c.Facturas)
                .FirstOrDefaultAsync(m => m.IdCliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Estadísticas del cliente
            ViewBag.TotalCompras = cliente.Ventas.Count;
            ViewBag.TotalGastado = cliente.Ventas.Sum(v => v.Total);
            ViewBag.UltimaCompra = cliente.Ventas.OrderByDescending(v => v.FechaVenta).FirstOrDefault()?.FechaVenta;
            ViewBag.TicketPromedio = cliente.Ventas.Any() ? cliente.Ventas.Average(v => v.Total) : 0;

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombres,Apellidos,TipoDocumento,NumeroDocumento,CorreoElectronico")] Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no exista un cliente con el mismo número de documento
                    var existeDocumento = await _context.Clientes
                        .AnyAsync(c => c.NumeroDocumento == cliente.NumeroDocumento);

                    if (existeDocumento)
                    {
                        ModelState.AddModelError("NumeroDocumento", "Ya existe un cliente con este número de documento.");
                        return View(cliente);
                    }

                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Cliente creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al crear el cliente: {ex.Message}";
                    return View(cliente);
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombres,Apellidos,TipoDocumento,NumeroDocumento,CorreoElectronico")] Clientes cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no exista otro cliente con el mismo número de documento
                    var existeDocumento = await _context.Clientes
                        .AnyAsync(c => c.NumeroDocumento == cliente.NumeroDocumento && c.IdCliente != id);

                    if (existeDocumento)
                    {
                        ModelState.AddModelError("NumeroDocumento", "Ya existe otro cliente con este número de documento.");
                        return View(cliente);
                    }

                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Cliente actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al actualizar el cliente: {ex.Message}";
                    return View(cliente);
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            // Verificar si tiene ventas asociadas
            var tieneVentas = await _context.Ventas.AnyAsync(v => v.IdCliente == id);
            ViewBag.TieneVentas = tieneVentas;

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                
                if (cliente == null)
                {
                    return NotFound();
                }

                // Verificar si tiene ventas asociadas
                var tieneVentas = await _context.Ventas.AnyAsync(v => v.IdCliente == id);
                
                if (tieneVentas)
                {
                    TempData["Error"] = "No se puede eliminar el cliente porque tiene ventas asociadas.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Cliente eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar el cliente: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // Método para exportar a Excel usando EPPlus
        public async Task<IActionResult> ExportarExcel()
        {
            var clientes = await _context.Clientes.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Clientes");
                ws.Cells[1,1].Value = "ID";
                ws.Cells[1,2].Value = "Nombre";
                ws.Cells[1,3].Value = "Apellidos";
                ws.Cells[1,4].Value = "TipoDocumento";
                ws.Cells[1,5].Value = "NumeroDocumento";
                ws.Cells[1,6].Value = "CorreoElectronico";

                int r = 2;
                foreach (var c in clientes)
                {
                    ws.Cells[r,1].Value = c.IdCliente;
                    ws.Cells[r,2].Value = c.Nombres;
                    ws.Cells[r,3].Value = c.Apellidos;
                    ws.Cells[r,4].Value = c.TipoDocumento;
                    ws.Cells[r,5].Value = c.NumeroDocumento;
                    ws.Cells[r,6].Value = c.CorreoElectronico;
                    r++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var ms = new MemoryStream();
                package.SaveAs(ms);
                ms.Position = 0;
                string fname = "Clientes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fname);
            }
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
