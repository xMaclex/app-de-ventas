using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;

namespace ventaapp.Controllers
{
    [Authorize]
    public class NcfController : Controller
    {
        private readonly VentasDbContext _context;

        public NcfController(VentasDbContext context)
        {
            _context = context;
        }

        // ─── GET /Ncf ────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            var secuencias = await _context.SecuenciaNcf
                .OrderBy(s => s.TipoComprobante)
                .ToListAsync();

            return View(secuencias);
        }

        // ─── GET /Ncf/Crear ──────────────────────────────────────────────────
        public IActionResult Crear()
        {
            return View(new SecuenciaNcf
            {
                FechaVencimiento = DateTime.Today.AddYears(1),
                NumeroInicial    = 1,
                NumeroFinal      = 10000,
                NumeroActual     = 1,
                Activa           = true
            });
        }

        // ─── POST /Ncf/Crear ─────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(SecuenciaNcf model)
        {
            if (model.NumeroFinal <= model.NumeroInicial)
                ModelState.AddModelError("NumeroFinal", "El número final debe ser mayor al número inicial.");

            if (model.FechaVencimiento.Date <= DateTime.Today)
                ModelState.AddModelError("FechaVencimiento", "La fecha de vencimiento debe ser futura.");

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var existeActiva = await _context.SecuenciaNcf
                    .AnyAsync(s => s.TipoComprobante == model.TipoComprobante && s.Activa);

                if (existeActiva)
                {
                    ModelState.AddModelError("TipoComprobante",
                        $"Ya existe una secuencia activa para el tipo {model.TipoComprobante}. " +
                        "Desactívela antes de crear una nueva.");
                    return View(model);
                }

                model.NumeroActual = model.NumeroInicial;
                model.UlitmoNumero  = model.NumeroInicial;

                _context.SecuenciaNcf.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Secuencia NCF {model.Descripcion} creada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al crear la secuencia: {ex.Message}";
                return View(model);
            }
        }

        // ─── GET /Ncf/Editar/5 ───────────────────────────────────────────────
        public async Task<IActionResult> Editar(int id)
        {
            var secuencia = await _context.SecuenciaNcf.FindAsync(id);
            if (secuencia == null) return NotFound();
            return View(secuencia);
        }

        // ─── POST /Ncf/Editar/5 ──────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, SecuenciaNcf model)
        {
            if (id != model.IdSecuencia) return NotFound();

            if (model.NumeroFinal <= model.NumeroInicial)
                ModelState.AddModelError("NumeroFinal", "El número final debe ser mayor al número inicial.");

            if (model.NumeroActual < model.NumeroInicial)
                ModelState.AddModelError("NumeroActual", "El número actual no puede ser menor al inicial.");

            if (model.NumeroActual > model.NumeroFinal)
                ModelState.AddModelError("NumeroActual", "El número actual no puede superar el número final.");

            if (!ModelState.IsValid) return View(model);

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Secuencia NCF actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.SecuenciaNcf.AnyAsync(s => s.IdSecuencia == id))
                    return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al actualizar: {ex.Message}";
                return View(model);
            }
        }

        // ─── POST /Ncf/ToggleActiva/5 ────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActiva(int id)
        {
            var secuencia = await _context.SecuenciaNcf.FindAsync(id);
            if (secuencia == null) return NotFound();

            // Si se va a activar, desactivar las otras del mismo tipo
            if (!secuencia.Activa)
            {
                var otrasActivas = await _context.SecuenciaNcf
                    .Where(s => s.TipoComprobante == secuencia.TipoComprobante
                             && s.IdSecuencia != id
                             && s.Activa)
                    .ToListAsync();

                foreach (var otra in otrasActivas)
                    otra.Activa = false;
            }

            secuencia.Activa = !secuencia.Activa;
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Secuencia {(secuencia.Activa ? "activada" : "desactivada")} correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // ─── POST /Ncf/Eliminar/5 ────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var secuencia = await _context.SecuenciaNcf.FindAsync(id);
                if (secuencia == null) return NotFound();

                if (secuencia.NumeroActual > secuencia.NumeroInicial)
                {
                    TempData["Error"] = "No se puede eliminar una secuencia que ya ha sido utilizada.";
                    return RedirectToAction(nameof(Index));
                }

                _context.SecuenciaNcf.Remove(secuencia);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Secuencia NCF eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // ─── API: próximo NCF para un tipo ───────────────────────────────────
        // GET /Ncf/ProximoNcf?tipo=B02
        [HttpGet]
        public async Task<JsonResult> ProximoNcf(string tipo)
        {
            var secuencia = await _context.SecuenciaNcf
                .Where(s => s.TipoComprobante == tipo && s.Activa)
                .FirstOrDefaultAsync();

            if (secuencia == null)
                return Json(new { ok = false, mensaje = $"No hay secuencia activa para el tipo {tipo}." });

            if (secuencia.Vencida)
                return Json(new { ok = false, mensaje = $"La secuencia {tipo} está vencida." });

            if (secuencia.Agotada)
                return Json(new { ok = false, mensaje = $"La secuencia {tipo} está agotada." });

            var ncf = $"{secuencia.TipoComprobante}{secuencia.NumeroActual:D10}";
            return Json(new
            {
                ok          = true,
                ncf,
                disponibles = secuencia.Disponibles,
                vencimiento = secuencia.FechaVencimiento.ToString("dd/MM/yyyy")
            });
        }

        // ─── API interna: consumir (incrementar) un NCF ───────────────────────
        // Llamar desde VentasController al guardar una venta
        // POST /Ncf/ConsumirNcf
        [HttpPost]
        public async Task<JsonResult> ConsumirNcf([FromBody] ConsumirNcfRequest req)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var secuencia = await _context.SecuenciaNcf
                    .Where(s => s.TipoComprobante == req.Tipo 
                            && s.Activa
                            && s.FechaVencimiento >= DateTime.Today
                            && s.NumeroActual <= s.NumeroFinal)
                    .FirstOrDefaultAsync();

                if (secuencia == null)
                    return Json(new { ok = false, mensaje = "No hay secuencia NCF disponible." });

                var ncf = secuencia.GenerarProximoNcf();

                if (ncf == null)
                {
                    await transaction.RollbackAsync();
                    return Json(new { ok = false, mensaje = "La secuencia está agotada o vencida." });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { ok = true, ncf });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { ok = false, mensaje = ex.Message });
            }
        }
    }

    public class ConsumirNcfRequest
    {
        public string Tipo { get; set; } = "B02";
    }
}