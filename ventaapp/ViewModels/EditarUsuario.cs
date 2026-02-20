using System.ComponentModel.DataAnnotations;

namespace ventaapp.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        // ─── Campos ÚNICOS (solo lectura, no se editan) ───────────────────────
        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Número de Documento")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Display(Name = "Tipo de Documento")]
        public string TipoDocumento { get; set; } = string.Empty;

        // ─── Campos EDITABLES ─────────────────────────────────────────────────
        [Required(ErrorMessage = "Digite el nombre")]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres")]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite los apellidos")]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden exceder los 50 caracteres")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Digite su Correo Electronico.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el número de teléfono.")]
        [StringLength(20, ErrorMessage = "Número de teléfono mal digitado, max: 20")]
        [Display(Name = "Número de Teléfono")]

        public string NumeroTelefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el número celular.")]
        [StringLength(20, ErrorMessage = "Número celular mal digitado, max: 20")]
        [Display(Name = "Número Celular")]
        public string NumeroCelular { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";

        // ─── Imagen ───────────────────────────────────────────────────────────
        [Display(Name = "Foto de Perfil")]
        public IFormFile? FotoPerfil { get; set; }

        public byte[]? FotoPerfilActual { get; set; }

        // ─── Info de solo lectura ─────────────────────────────────────────────
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public bool EstaBloqueado { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
    }
}