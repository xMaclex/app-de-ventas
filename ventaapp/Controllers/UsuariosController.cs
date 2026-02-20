/* using Microsoft.AspNetCore.Mvc;
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

        //GET Usuarios
        public async Task<IActionResult> Index(string searchString, string tipoDocumento)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["TipoDocumentoFilter"] = tipoDocumento;

            var usuarios = from u in _context.Usuario
                            select u;

            // Filtro por búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                    usuarios = Usuarios.Where(u => 
                    u.Nombres.Contains(searchString) ||
                    u.Apellidos.Contains(searchString) ||
                    u.NumeroDocumento.Contains(searchString) ||
                    u.CorreoElectronico.Contains(searchString));
            }

            //filtrar por tipo de documento
            if (!string.IsNullOrEmpty(tipoDocumento))
            {
                usuarios = usuarios.Where(u => u.TipoDocumento == tipoDocumento);
            }

            var UsuariosList = await usuarios.OrderBy(u => u.Nombre).ToListAsync();

             // Estadísticas para el dashboard
            ViewBag.TotalUsuarios = await _context.Usuario.CountAsync();
            ViewBag.UsuariosActivos = UsuariosList.Count;

            return View(UsuariosList);
        }


    }
}

*/