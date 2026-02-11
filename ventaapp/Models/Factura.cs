using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ventaapp.Models;

[Table("facturas_tb")]
public class Factura
{
    [Key]
    [Column("id_factura")]
    public int IdFactura { get; set; }
    
    [Column("id_venta")]
    public int IdVenta { get; set; }
    
    [Column("id_cliente")]
    public int IdCliente { get; set; }
    
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_usuarios")]
    public int IdUsuarios { get; set; }
    
    [Column("numero_factura")]
    public string NumeroFactura { get; set; } = string.Empty;
    
    [Column("fecha_emision")]
    public DateTime FechaEmision { get; set; }
    
    [Column("rnc_empresa")]
    public string RncEmpresa { get; set; } = string.Empty;
    
    [Column("nombre_empresa")]
    public string NombreEmpresa { get; set; } = string.Empty;
    
    [Column("direccion_empresa")]
    public string DireccionEmpresa { get; set; } = string.Empty;
    
    [Column("ncf")]
    public string Ncf { get; set; } = string.Empty;
    
    [Column("tipo_comprobante_fiscal")]
    public string TipoComprobanteFiscal { get; set; } = string.Empty;
    
    [Column("estado")]
    public string Estado { get; set; } = string.Empty;

  // relaciones con las tablas
    public Venta Venta {get; set;} = new Venta();
    public Clientes Cliente { get; set; } = new Clientes();
    public Producto Producto { get; set; } = new Producto();
    public Usuarios Usuario { get; set; } = new Usuarios();

}