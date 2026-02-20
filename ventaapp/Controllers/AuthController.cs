using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ventaapp.Data;
using ventaapp.Models;
using ventaapp.ViewModels;

namespace ventaapp.Controllers
{
    public class AuthController : Controller
    {
        private readonly VentasDbContext _context;
        private const int MAX_INTENTOS_FALLIDOS = 5;
        private const int MINUTOS_BLOQUEO = 30;

        public AuthController(VentasDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            // Si ya está autenticado, redirigir al Home
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Buscar usuario por username
                var usuario = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                    return View(model);
                }

                // Verificar si está bloqueado
                if (usuario.EstaBloqueado)
                {
                    ModelState.AddModelError(string.Empty, 
                        $"Usuario bloqueado. Intente nuevamente después de {usuario.BloqueadoHasta:dd/MM/yyyy HH:mm}");
                    return View(model);
                }

                // Verificar si está activo
                if (!usuario.EstaActivo)
                {
                    ModelState.AddModelError(string.Empty, "Usuario inactivo. Contacte al administrador");
                    return View(model);
                }

                // Verificar contraseña
                if (!VerificarPassword(model.Password, usuario.Clave))
                {
                    // Incrementar intentos fallidos
                    usuario.IntentosFallidos++;

                    if (usuario.IntentosFallidos >= MAX_INTENTOS_FALLIDOS)
                    {
                        usuario.BloqueadoHasta = DateTime.Now.AddMinutes(MINUTOS_BLOQUEO);
                        await _context.SaveChangesAsync();
                        
                        ModelState.AddModelError(string.Empty, 
                            $"Usuario bloqueado por {MINUTOS_BLOQUEO} minutos debido a múltiples intentos fallidos");
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                        ModelState.AddModelError(string.Empty, 
                            $"Usuario o contraseña incorrectos. Intentos restantes: {MAX_INTENTOS_FALLIDOS - usuario.IntentosFallidos}");
                    }

                    return View(model);
                }

                // Login exitoso - Resetear intentos fallidos
                usuario.IntentosFallidos = 0;
                usuario.BloqueadoHasta = null;
                usuario.UltimoAcceso = DateTime.Now;
                await _context.SaveChangesAsync();

                // Crear claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()), 
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Redirigir
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al iniciar sesión: " + ex.Message);
                return View(model);
            }
        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            // Si ya está autenticado, redirigir al Home
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                //valida la foto de perfil
                byte[]? fotoPerfilBytes = null;
                if (model.FotoPerfil != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.FotoPerfil.CopyToAsync(memoryStream);
                            fotoPerfilBytes = memoryStream.ToArray();
                    }
                }

                // Verificar si el username ya existe
                if (await _context.Usuario.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "El nombre de usuario ya está en uso");
                    return View(model);
                }

                // Verificar si el email ya existe
                if (await _context.Usuario.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado");
                    return View(model);
                }

                // Verificar si el documento ya existe
                if (await _context.Usuario.AnyAsync(u => u.NumeroDocumento == model.NumeroDocumento))
                {
                    ModelState.AddModelError("NumeroDocumento", "El número de documento ya está registrado");
                    return View(model);
                }

                // Crear nuevo usuario
                var nuevoUsuario = new Usuarios
                {
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    TipoDocumento = model.TipoDocumento,
                    NumeroDocumento = model.NumeroDocumento,
                    NumeroTelefono = model.NumeroTelefono,
                    NumeroCelular = model.NumeroCelular,
                    Username = model.Username,
                    Email = model.Email,
                    Clave = HashPassword(model.Password),
                    Estado = "Activo",
                                            // Por defecto todos los nuevos usuarios son vendedores
                    FechaRegistro = DateTime.Now,
                    IntentosFallidos = 0,
                    FotoPerfil = fotoPerfilBytes,
                    
                };

                _context.Usuario.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registro exitoso. Por favor inicie sesión.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al registrar usuario: " + ex.Message);
                return View(model);
            }
        }
        
        // GET: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
            public IActionResult Index()
            {
                return RedirectToAction(nameof(Login));
            }

        // GET: Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Métodos de Hash de Contraseña

        // Hash de contraseña usando SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Verificar contraseña
        private bool VerificarPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }

        #endregion
    }
}