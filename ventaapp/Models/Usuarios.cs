using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models
{
    [Table("usuarios_tb")]
    public class Usuarios
    {
        [Key]
        [Column("id_usuarios")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Digite el nombre")]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres")]
        [Display(Name = "Nombres")]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite los apellidos")]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden exceder los 50 caracteres")]
        [Display(Name = "Apellidos")]
        [Column("apellidos")]
        public string Apellidos { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Seleccione el tipo de documento")]
        [Display(Name = "Tipo de Documento")]
        [Column("tipo_documento")]
        [StringLength(50)]
        public string TipoDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el numero de documento correspondiente.")]
        [StringLength(20, ErrorMessage = "El numero de documento no pueden exceder los 20 caracteres")]
        [Display(Name = "Numero de Documento")]
        [Column("numero_documento")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el numero de telefono.")]
        [StringLength(20, ErrorMessage = "numero de telefono mal digitado, max: 20")]
        [Display(Name = "Numero de Telefono")]
        [Column("numero_telefono")]
        public string NumeroTelefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el numero celular.")]
        [StringLength(20, ErrorMessage = "numero celular mal digitado, max: 20")]
        [Display(Name = "Numero Celular")]
        [Column("numero_celular")]
        public string NumeroCelular { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su UserName.")]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su Contraseña.")]
        [StringLength(255, ErrorMessage = "La contraseña no puede exceder los 255 caracteres")]
        [Display(Name = "Contraseña")]
        [Column("clave")]
        public string Clave { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su Correo Electronico.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Display(Name = "Correo Electronico")]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        [Required(ErrorMessage = "Rol obligatorio")]
        [StringLength(20)]
        [Column("id_roles")]
        public int IdRoles { get; set; }

        [Display(Name = "Fecha de Registro")]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Ultimo Acceso al Sistema")]
        [Column("ultimo_acceso")]
        public DateTime? UltimoAcceso { get; set; }

        [Column("intentos_fallidos")]
        public int IntentosFallidos { get; set; } = 0;

        [Column("bloqueado_hasta")]
        public DateTime? BloqueadoHasta { get; set; }

        // Propiedades calculadas
        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellidos}";

        [NotMapped]
        public bool EstaBloqueado =>
        BloqueadoHasta.HasValue && BloqueadoHasta > DateTime.Now;

       [NotMapped]
        public bool EstaActivo => !EstaBloqueado;

        // Relaciones entre tablas
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        //[ForeignKey("id_roles")]
       // public Roles? Roles{ get; set; }
    }
}