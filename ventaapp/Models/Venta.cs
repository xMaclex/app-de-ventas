namespace ventaapp.Models;

public class Venta
{
    public int IdVenta { get; set;}
    public DateTime Fechaventa { get; set;}
    public int IdCliente { get; set;}
    public decimal Subtotal { get; set;}
    public decimal Itbis { get; set;}
    public decimal Total { get; set;}
    public string MetodoPago { get; set;} = string.Empty;
    public string TipoComprobante { get; set;} = string.Empty;
    public string NumeroComprobante {get; set;} = string.Empty;
    public string Estado {get; set;} = string.Empty;
    
  // relaciones con las tablas
    public Clientes Cliente { get; set; } = new Clientes();
    public ICollection<Factura> Facturas { get; set;} = new List<Factura>();
}