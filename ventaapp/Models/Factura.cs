using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models
{
    [Table("facturas_tb")]
    public class Factura
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El ID de venta es obligatorio")]
        [Display(Name = "Venta")]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El ID de cliente es obligatorio")]
        [Display(Name = "Cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El ID de producto es obligatorio")]
        [Display(Name = "Producto")]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio")]
        [StringLength(30)]
        [Display(Name = "Número de Factura")]
        [Column("numero_factura")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de emisión es obligatoria")]
        [Display(Name = "Fecha de Emisión")]
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El RNC de la empresa es obligatorio")]
        [StringLength(30)]
        [Display(Name = "RNC Empresa")]
        [Column("rnc_empresa")]
        public string RncEmpresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Nombre Empresa")]
        [Column("nombre_empresa")]
        public string NombreEmpresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección de la empresa es obligatoria")]
        [StringLength(100)]
        [Display(Name = "Dirección Empresa")]
        [Column("direccion_empresa")]
        public string DireccionEmpresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "El NCF es obligatorio")]
        [StringLength(20)]
        [Display(Name = "NCF")]
        [Column("ncf")]
        public string Ncf { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de comprobante fiscal es obligatorio")]
        [StringLength(20)]
        [Display(Name = "Tipo de Comprobante Fiscal")]
        [Column("tipo_comprobante_fiscal")]
        public string TipoComprobanteFiscal { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Activa";

        // Campos adicionales para gestión avanzada
        [Display(Name = "Motivo de Anulación")]
        [StringLength(500)]
        [Column("motivo_anulacion")]
        public string MotivoAnulacion { get; set; } = string.Empty;

        [Display(Name = "Fecha de Anulación")]
        [Column("fecha_anulacion")]
        public DateTime? FechaAnulacion { get; set; }

        [Display(Name = "Monto Total")]
        [Column("monto_total")]
        [DataType(DataType.Currency)]
        public decimal MontoTotal { get; set; }

        [Display(Name = "ITBIS")]
        [Column("monto_itbis")]
        [DataType(DataType.Currency)]
        public decimal MontoItbis { get; set; }

        [Display(Name = "ID del usuario")]
        [Column("id_usuarios")]
        [ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }

        // Relaciones con las tablas
        public Venta Venta { get; set; } = new Venta();
        public Clientes Cliente { get; set; } = new Clientes();
        public Producto Producto { get; set; } = new Producto();
        public Usuarios Usuario { get; set; } = new Usuarios();

        // Propiedades calculadas
        [NotMapped]
        public string EstadoDescripcion
        {
            get
            {
                return Estado switch
                {
                    "Activa" => "Factura Activa",
                    "Anulada" => $"Anulada - {MotivoAnulacion}",
                    _ => "Estado Desconocido"
                };
            }
        }

        [NotMapped]
        public string TipoNCFDescripcion
        {
            get
            {
                return TipoComprobanteFiscal switch
                {
                    "B01" => "B01 - Crédito Fiscal",
                    "B02" => "B02 - Consumidor Final",
                    "B14" => "B14 - Régimen Especial",
                    "B15" => "B15 - Gubernamental",
                    _ => TipoComprobanteFiscal
                };
            }
        }
    }

    // Clase para gestión de secuencias de NCF
    public class SecuenciaNcf
    {
        [Key]
        public int IdSecuencia { get; set; }

        [Required]
        [StringLength(3)]
        public string TipoComprobante { get; set; } = string.Empty;

        [Required]
        public long NumeroInicial { get; set; }

        [Required]
        public long NumeroFinal { get; set; }

        [Required]
        public long NumeroActual { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        public bool Activa { get; set; } = true;

        [NotMapped]
        public long Disponibles => NumeroFinal - NumeroActual;

        [NotMapped]
        public decimal PorcentajeUsado => ((decimal)(NumeroActual - NumeroInicial) / (NumeroFinal - NumeroInicial)) * 100;

        [NotMapped]
        public bool CercaDeAgotarse => Disponibles < 100;

        [NotMapped]
        public bool Vencida => DateTime.Now > FechaVencimiento;
    }
}
