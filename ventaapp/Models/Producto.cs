namespace ventaapp.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string CodigoProducto { get; set; } = string.Empty;
    public string NombreProducto { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal PrecioCompra { get; set; }
     public decimal PrecioVenta{ get; set; }
     public decimal Impuesto { get; set; }
     public string Estado { get; set; } = string.Empty;

  // relaciones con las tablas
     public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}