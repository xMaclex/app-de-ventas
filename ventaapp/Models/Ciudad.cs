using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models;

[Table("ciudades_tb")]
public class Ciudad
{
    [Key]
    [Column("id_ciudad")]
    public int IdCiudad { get; set; }

    [Required(ErrorMessage = "El nombre de la ciudad es obligatorio")]
    [StringLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("id_pais")]
    public int IdPais { get; set; }

    // Relaciones
    [ForeignKey("IdPais")]
    public Pais Pais { get; set; } = new Pais();

    public ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();
}
