 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ventaapp.Models
{
    [Table("ventas_db")]

    public class Usuarios
    {
        [Key]
        [Column("id_usuarios")]
        public int idUsuario { get; set; }

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
        public string NumeroTelefono{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite el numero celular.")]
        [StringLength(20, ErrorMessage = "numero celular mal digitado, max: 20")]
        [Display(Name = "Numero Celular")]
        [Column("numero_celular")]
        public string NumeroCelular{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su UserName.")]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        [Column("username")]
        public string Username{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su Contraseña.")]
        [StringLength(255, ErrorMessage = "Los nombres no pueden exceder los 255 caracteres")]
        [Display(Name = "Contraseña")]
        [Column("clave")]
        public string Clave{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite su Correo Electronico.")]
        [StringLength(100, ErrorMessage = "Los nombres no pueden exceder los 100 caracteres")]
        [Display(Name = "Correo Electronico")]
        [Column("email")]
        public string Email{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        [Required(ErrorMessage = "Estado obligatorio")]
        [StringLength(20)]
        [Column("rol")]
        public string Rol { get; set; } = " ";

        [Display(Name = "Fecha de Registro")]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Display(Name = "Ultimo Acceso al Sistema")]
        [Column("ultimo_accesoo")]
        public DateTime? UltimoAcceso { get; set; } = DateTime.UtcNow;

        





        

    }
}

