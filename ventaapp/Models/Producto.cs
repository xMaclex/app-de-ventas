using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ventaapp.Models;

[Table("productos_tb")]
public class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }
    
    [Column("codigo_producto")]
    public string CodigoProducto { get; set; } = string.Empty;
    
    [Column("nombre_producto")]
    public string NombreProducto { get; set; } = string.Empty;
    
    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    
    [Column("categoria")]
    public string Categoria { get; set; } = string.Empty;
    
    [Column("precio_compra")]
    public decimal PrecioCompra { get; set; }

    [Column ("precio_venta")]
     public decimal PrecioVenta{ get; set; }

     [Column ("impuesto")]
     public decimal Impuesto { get; set; }

     [Column ("estado")]
     public string Estado { get; set; } = string.Empty;

  // relaciones con las tablas
     public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}