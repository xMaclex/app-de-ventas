using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models
{
    [Table("ventas_tb")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "La fecha de venta es obligatoria")]
        [Display(Name = "Fecha de Venta")]
        [Column("fecha_venta")]
        public DateTime Fechaventa { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        [Display(Name = "Cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio")]
        [Range(0.01, 999999999.99, ErrorMessage = "El subtotal debe ser mayor a 0")]
        [Display(Name = "Subtotal")]
        [Column("subtotal")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "El ITBIS es obligatorio")]
        [Range(0, 999999999.99, ErrorMessage = "El ITBIS debe ser mayor o igual a 0")]
        [Display(Name = "ITBIS")]
        [Column("itbis")]
        [DataType(DataType.Currency)]
        public decimal Itbis { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(0.01, 999999999.99, ErrorMessage = "El total debe ser mayor a 0")]
        [Display(Name = "Total")]
        [Column("total")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        [StringLength(30)]
        [Display(Name = "Método de Pago")]
        [Column("metodo_pago")]
        public string MetodoPago { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un tipo de comprobante")]
        [StringLength(30)]
        [Display(Name = "Tipo de Comprobante")]
        [Column("tipo_comprobante")]
        public string TipoComprobante { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Número de Comprobante")]
        [Column("numero_comprobante")]
        public string NumeroComprobante { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Completada";

        // Campos adicionales para funcionalidades avanzadas
        [Display(Name = "Descuento")]
        [Column("descuento")]
        [DataType(DataType.Currency)]
        public decimal Descuento { get; set; } = 0;

        [Display(Name = "Tipo de Descuento")]
        [StringLength(20)]
        [Column("tipo_descuento")]
        public string TipoDescuento { get; set; } = "Monto"; // "Monto" o "Porcentaje"

        [Display(Name = "Tipo de Venta")]
        [StringLength(20)]
        [Column("tipo_venta")]
        public string TipoVenta { get; set; } = "Contado"; // "Contado" o "Crédito"

        [Display(Name = "Notas")]
        [StringLength(500)]
        [Column("notas")]
        public string Notas { get; set; } = string.Empty;

        [Display(Name = "Usuario: ")]
        [Column("id_usuarios")]
        public int IdUsuario { get; set; }

        // Relaciones con las tablas
        public Clientes Cliente { get; set; } = new Clientes();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        public Usuarios Usuario { get; set; } = new Usuarios();
        
        // Propiedades de navegación adicionales (para el carrito)
        [NotMapped]
        public List<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }

    // Clase auxiliar para manejar el detalle de la venta (carrito)
    public class VentaDetalle
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
        public decimal MontoImpuesto => Subtotal * (Impuesto / 100);
        public decimal Total => Subtotal + MontoImpuesto;
    }

    // ViewModel para el punto de venta
    public class PuntoVentaViewModel
    {
        public Venta Venta { get; set; } = new Venta();
        public List<VentaDetalle> Carrito { get; set; } = new List<VentaDetalle>();
        public List<Clientes> Clientes { get; set; } = new List<Clientes>();
        public List<Producto> Productos { get; set; } = new List<Producto>();
        
        // Propiedades calculadas
        public decimal SubtotalCarrito => Carrito.Sum(d => d.Subtotal);
        public decimal ItbisCarrito => Carrito.Sum(d => d.MontoImpuesto);
        public decimal TotalCarrito => Carrito.Sum(d => d.Total);
        public decimal DescuentoCalculado
        {
            get
            {
                if (Venta.TipoDescuento == "Porcentaje")
                    return SubtotalCarrito * (Venta.Descuento / 100);
                else
                    return Venta.Descuento;
            }
        }
        public decimal TotalFinal => TotalCarrito - DescuentoCalculado;
    }
}
