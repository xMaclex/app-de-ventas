using Microsoft.AspNetCore.Mvc;

namespace ventaapp.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AjustesController : Controller
    {
        public IActionResult Editar()
        {
            return View();
        }
    }
}