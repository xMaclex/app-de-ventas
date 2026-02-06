namespace ventaapp.Models;

public class Factura
{
    public int IdFactura { get; set; }
    public int IdVenta { get; set; }
    public int IdCliente { get; set; }
    public int IdProducto { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    public string RncEmpresa { get; set; } = string.Empty;
    public string NombreEmpresa { get; set; } = string.Empty;
    public string DireccionEmpresa { get; set; } = string.Empty;
    public string Ncf { get; set; } = string.Empty;
    public string TipoComprobanteFiscal { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;

  // relaciones con las tablas
    public Venta Venta {get; set;} = new Venta();
    public Clientes Cliente { get; set; } = new Clientes();
    public Producto Producto { get; set; } = new Producto();

}