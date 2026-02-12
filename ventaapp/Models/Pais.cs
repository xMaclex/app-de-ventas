using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models;

[Table("paises_tb")]
public class Pais
{
    [Key]
    [Column("id_pais")]
    public int IdPais { get; set; }

    [Required(ErrorMessage = "El nombre del país es obligatorio")]
    [StringLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    // Relaciones
    public ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();
    public ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();
}
