using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ventaapp.Models;

[Table("clientes_tb")]
public class Clientes
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Digite el nombre")]
    [StringLength(100, ErrorMessage = "Los nombres no pueden exceder los 100 caracteres")]
    [Column("nombres")]
    public string Nombres { get; set; } = string.Empty;


    [Required(ErrorMessage = "Digite sus apellidos")]
    [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder los 100 caracteres")]
    [Column("apellidos")]
    public string Apellidos { get; set; } = string.Empty;


    [Required(ErrorMessage = "Digite el documento correspondiente")]
    [StringLength(50)]
    [Column("tipo_documento")]
    public string TipoDocumento { get; set; } = string.Empty; 


    [Required(ErrorMessage = "Digite el documento correspondiente")]
    [StringLength(20)]
    [Column("numero_documento")]
    public string NumeroDocumento { get; set; } = string.Empty;


    [Required(ErrorMessage = "Email obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo electronico no es valido")]
    [StringLength(20)]
    [Column("correo_electronico")]
    public string CorreoElectronico { get; set; } = string.Empty;


    [NotMapped]
    public string NombreCompleto => $"{Nombres} {Apellidos}";

    // relaciones con las tablas
    
   public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
   public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}