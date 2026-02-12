using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;

namespace ventaapp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly VentasDbContext _context;

        public ProductosController(VentasDbContext context)
        {
            _context = context;
        }


// Get De productos
        public async Task<IActionResult> Index(string searchString,string categoria, string estado)
        {
            ViewData ["CurrentFilter"] = searchString;
            ViewData ["CategoriaFilter"] = categoria;
            ViewData ["EstadoFilter"] = estado;


            var productos = from p in _context.Productos 
                select p;

                //filtrar por busqueda
                if (!string .IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.NombreProducto.Contains(searchString) ||
                p.CodigoProducto.Contains(searchString) ||
                p.Descripcion.Contains(searchString)
                );
            }

             // Filtro por categoría
            if (!string.IsNullOrEmpty(categoria))
            {
                productos = productos.Where(p => p.Categoria == categoria);
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                productos = productos.Where(p => p.Estado == estado);
            }

            var productoList = await productos.OrderBy(p => p.NombreProducto).ToListAsync();


            //estadistica para el dashboard
            ViewBag.TotalProductos = await _context.Productos.CountAsync();
            ViewBag.ProductosActivos = await _context.Productos.CountAsync(p => p.Estado == "Activo");
            ViewBag.ProductosInactivos = await _context.Productos.CountAsync(p => p.Estado == "Inactivo");
            ViewBag.ValorInventario = await _context.Productos
            .Where(p => p.Estado == "Activo")
            .SumAsync(p => p.PrecioCompra);

            //obtener lista de categoria por filtros
            ViewBag.Categorias = await _context.Productos
            .Select(p => p.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

            return View(productoList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
            .Include(p => p.Facturas)
            .FirstOrDefaultAsync(m => m.IdProducto == id );


            if(producto == null)
            {
                return NotFound();
            }

             // Estadísticas del producto
            ViewBag.TotalVendido = producto.Facturas.Count();
            ViewBag.UltimaVenta = producto.Facturas
                .OrderByDescending(f => f.FechaEmision)
                .FirstOrDefault()?.FechaEmision;

                return View(producto);
        }
        //Get producto Greate
        public async Task<IActionResult> Create()
        {
            return View();
        }
        //Post producto Greate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,CodigoProducto,NombreProducto,Descripcion,Categoria,PrecioCompra,PrecioVenta,Impuesto,Estado" )] Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no exista un producto con el mismo código
                    var existeCodigo = await _context.Productos
                    .AnyAsync(p => p.CodigoProducto == producto.CodigoProducto);

                    if(existeCodigo)
                    {
                        ModelState.AddModelError("CodigoProducto", "Ya existe un producto con este código.");
                        return View(producto);
                    }

                    // Validar que el precio de venta sea mayor al de compra
                    if(producto.PrecioVenta <= producto.PrecioCompra)
                    {
                        ModelState.AddModelError("PrecioVenta", "El precio de venta debe ser mayor al precio de compra.");
                        return View(producto);
                    }

                    _context.Add(producto);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Producto creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al crear el producto: {ex.Message} ";
                    return View(producto);
                }
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,CodigoProducto,NombreProducto,Descripcion,Categoria,PrecioCompra,PrecioVenta,Impuesto,Estado")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no exista otro producto con el mismo código
                    var existeCodigo = await _context.Productos
                        .AnyAsync(p => p.CodigoProducto == producto.CodigoProducto && p.IdProducto != id);

                    if (existeCodigo)
                    {
                        ModelState.AddModelError("CodigoProducto", "Ya existe otro producto con este código.");
                        return View(producto);
                    }

                    // Validar que el precio de venta sea mayor al de compra
                    if (producto.PrecioVenta <= producto.PrecioCompra)
                    {
                        ModelState.AddModelError("PrecioVenta", "El precio de venta debe ser mayor al precio de compra.");
                        return View(producto);
                    }

                    _context.Update(producto);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Producto actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
                    TempData["Error"] = $"Error al actualizar el producto: {ex.Message}";
                    return View(producto);
                }
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);

            if (producto == null)
            {
                return NotFound();
            }

            // Verificar si tiene facturas asociadas
            var tieneFacturas = await _context.Facturas.AnyAsync(f => f.IdProducto == id);
            ViewBag.TieneFacturas = tieneFacturas;

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return NotFound();
                }

                // Verificar si tiene facturas asociadas
                var tieneFacturas = await _context.Facturas.AnyAsync(f => f.IdProducto == id);

                if (tieneFacturas)
                {
                    TempData["Error"] = "No se puede eliminar el producto porque tiene facturas asociadas.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Producto eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar el producto: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Productos/CambiarEstado/5
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            // Cambiar el estado
            producto.Estado = producto.Estado == "Activo" ? "Inactivo" : "Activo";

            await _context.SaveChangesAsync();

            TempData["Success"] = $"El producto ahora está {producto.Estado}.";
            return RedirectToAction(nameof(Index));
        }

        // Método para exportar a Excel (requiere EPPlus o similar)
        public async Task<IActionResult> ExportarExcel()
        {
            var productos = await _context.Productos.ToListAsync();

            // Aquí irá la lógica de exportación cuando instales EPPlus
            // Por ahora retorna un mensaje
            TempData["Info"] = "Función de exportación disponible próximamente.";
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para obtener estadísticas
        public async Task<IActionResult> Estadisticas()
        {
            var productos = await _context.Productos.ToListAsync();

            var stats = new
            {
                TotalProductos = productos.Count,
                ProductosActivos = productos.Count(p => p.Estado == "Activo"),
                ProductosInactivos = productos.Count(p => p.Estado == "Inactivo"),
                ValorInventarioCompra = productos.Where(p => p.Estado == "Activo").Sum(p => p.PrecioCompra),
                ValorInventarioVenta = productos.Where(p => p.Estado == "Activo").Sum(p => p.PrecioVenta),
                PorCategoria = productos.GroupBy(p => p.Categoria)
                    .Select(g => new { Categoria = g.Key, Cantidad = g.Count() })
                    .OrderByDescending(x => x.Cantidad)
                    .ToList()
            };

            return Json(stats);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}