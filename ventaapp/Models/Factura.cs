using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models;

[Table("facturas_tb")]
public class Factura
{
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Required]
        [Display(Name = "Venta")]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Producto")]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Número de Factura")]
        [Column("numero_factura")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Fecha de Emisión")]
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        [Required]
        [StringLength(30)]
        [Display(Name = "RNC Empresa")]
        [Column("rnc_empresa")]
        public string? RncEmpresa { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Empresa")]
        [Column("nombre_empresa")]
        public string? NombreEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Dirección Empresa")]
        [Column("direccion_empresa")]
        public string? DireccionEmpresa { get; set; }

        /// <summary>
        /// NCF completo, ej: "B020000000001"
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "NCF")]
        [Column("ncf")]
        public string? Ncf { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Tipo de Comprobante Fiscal")]
        [Column("tipo_comprobante_fiscal")]
        public string TipoComprobanteFiscal { get; set; } = string.Empty;

        /// <summary>
        /// FK a secuencias_ncf_tb.id_secuencia
        /// </summary>
        [Display(Name = "Secuencia NCF")]
        [Column("id_secuencia_ncf")]
        public int? IdSecuenciaNcf { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Activa";

        [StringLength(500)]
        [Display(Name = "Motivo de Anulación")]
        [Column("motivo_anulacion")]
        public string? MotivoAnulacion { get; set; }

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
        public int IdUsuario { get; set; }

        // ── Navegación ──────────────────────────────────────────────────
        public Venta? Venta { get; set; }
        public Clientes? Cliente { get; set; }
        public Producto? Producto { get; set; }
        public Usuarios? Usuario { get; set; }

        [ForeignKey("IdSecuenciaNcf")]
        public SecuenciaNcf? SecuenciaNcf { get; set; }

        // ── Propiedades calculadas ───────────────────────────────────────
        [NotMapped]
        public string EstadoDescripcion => Estado switch
        {
            "Activa"  => "Factura Activa",
            "Anulada" => $"Anulada - {MotivoAnulacion}",
            _         => "Estado Desconocido"
        };

        [NotMapped]
        public string TipoNCFDescripcion => TipoComprobanteFiscal switch
        {
            "B01" => "B01 - Crédito Fiscal",
            "B02" => "B02 - Consumidor Final",
            "B14" => "B14 - Régimen Especial",
            "B15" => "B15 - Gubernamental",
            _     => TipoComprobanteFiscal
        };
}