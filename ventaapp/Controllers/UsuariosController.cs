using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using ventaapp.Models;
using ventaapp.ViewModels;

namespace ventaapp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly VentasDbContext _context;

        public UsuariosController(VentasDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET: /Usuarios
        // Lista de todos los usuarios
        // ─────────────────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario.ToListAsync();
            return View(usuarios);
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET: /Usuarios/Editar/5
        // ─────────────────────────────────────────────────────────────────────
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            var vm = new EditarUsuarioViewModel
            {
                IdUsuario        = usuario.IdUsuario,
                Username         = usuario.Username,
                Email            = usuario.Email,
                NumeroDocumento  = usuario.NumeroDocumento,
                TipoDocumento    = usuario.TipoDocumento,
                Nombre           = usuario.Nombre,
                Apellidos        = usuario.Apellidos,
                NumeroTelefono   = usuario.NumeroTelefono,
                NumeroCelular    = usuario.NumeroCelular,
                Estado           = usuario.Estado,
                FechaRegistro    = usuario.FechaRegistro,
                UltimoAcceso     = usuario.UltimoAcceso,
                EstaBloqueado    = usuario.EstaBloqueado,
                FotoPerfilActual = usuario.FotoPerfil
            };

            return View(vm);
        }

        // ─────────────────────────────────────────────────────────────────────
        // POST: /Usuarios/Editar/5
        // ─────────────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EditarUsuarioViewModel vm)
        {
            if (id != vm.IdUsuario)
                return NotFound();

            if (!ModelState.IsValid)
                return View(vm);

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            // Actualizar campos editables
            usuario.Nombre         = vm.Nombre;
            usuario.Apellidos      = vm.Apellidos;
            usuario.NumeroTelefono = vm.NumeroTelefono;
            usuario.NumeroCelular  = vm.NumeroCelular;
            usuario.Estado         = vm.Estado;
            usuario.Email          = vm.Email;

            // Procesar imagen si se subió una nueva
            if (vm.FotoPerfil != null && vm.FotoPerfil.Length > 0)
            {
                // Validar tipo de archivo
                var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (!tiposPermitidos.Contains(vm.FotoPerfil.ContentType.ToLower()))
                {
                    ModelState.AddModelError("FotoPerfil", "Solo se permiten imágenes JPG, PNG, GIF o WEBP.");
                    vm.FotoPerfilActual = usuario.FotoPerfil;
                    return View(vm);
                }

                // Validar tamaño (máximo 2MB)
                if (vm.FotoPerfil.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("FotoPerfil", "La imagen no puede superar los 2MB.");
                    vm.FotoPerfilActual = usuario.FotoPerfil;
                    return View(vm);
                }

                using var ms = new MemoryStream();
                await vm.FotoPerfil.CopyToAsync(ms);
                usuario.FotoPerfil = ms.ToArray();
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Usuario <strong>{usuario.NombreCompleto}</strong> actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuario.Any(u => u.IdUsuario == id))
                    return NotFound();
                throw;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET: /Usuarios/FotoPerfil/5
        // Devuelve la imagen del usuario como respuesta HTTP (para usar en <img src="">)
        // ─────────────────────────────────────────────────────────────────────
        public async Task<IActionResult> FotoPerfil(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario?.FotoPerfil == null || usuario.FotoPerfil.Length == 0)
            {
                // Retornar imagen por defecto (placeholder SVG)
                var svg = @"<svg xmlns='http://www.w3.org/2000/svg' width='120' height='120' viewBox='0 0 120 120'>
                    <rect width='120' height='120' fill='#e8edf5' rx='60'/>
                    <circle cx='60' cy='45' r='22' fill='#b0bdd0'/>
                    <ellipse cx='60' cy='95' rx='35' ry='25' fill='#b0bdd0'/>
                </svg>";
                return File(System.Text.Encoding.UTF8.GetBytes(svg), "image/svg+xml");
            }

            return File(usuario.FotoPerfil, "image/jpeg");
        }

        // ─────────────────────────────────────────────────────────────────────
        // POST: /Usuarios/EliminarFoto/5
        // Elimina la foto de perfil del usuario
        // ─────────────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarFoto(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.FotoPerfil = null;
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Foto de perfil eliminada correctamente.";
            return RedirectToAction(nameof(Editar), new { id });
        }

        //GET: Usuarios/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        //POST Usuario/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmar(int id)
        {
            try
            {
                var usuarios = await _context.Usuario.FindAsync(id);

                if (usuarios == null)
                {
                    return NotFound();
                }

                _context.Usuario.Remove(usuarios);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Usuario eliminado correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error al eliminar el Usuario: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}