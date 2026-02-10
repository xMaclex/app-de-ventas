using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ventaapp.Models;

[Table("ventas_tb")]
public class Venta
{
    [Key]
    [Column("id_venta")]
    public int IdVenta { get; set;}
    
    [Column("fecha_venta")]
    public DateTime Fechaventa { get; set;}
    
    [Column("id_cliente")]
    public int IdCliente { get; set;}
    
    [Column("subtotal")]
    public decimal Subtotal { get; set;}
    
    [Column("itbis")]
    public decimal Itbis { get; set;}
    
    [Column("total")]
    public decimal Total { get; set;}
    
    [Column("metodo_pago")]
    public string MetodoPago { get; set;} = string.Empty;
    
    [Column("tipo_comprobante")]
    public string TipoComprobante { get; set;} = string.Empty;
    
    [Column("numero_comprobante")]
    public string NumeroComprobante {get; set;} = string.Empty;
    
    [Column("estado")]
    public string Estado {get; set;} = string.Empty;
    
  // relaciones con las tablas
    public Clientes Cliente { get; set; } = new Clientes();
    public ICollection<Factura> Facturas { get; set;} = new List<Factura>();
}