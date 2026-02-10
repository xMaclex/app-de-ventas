using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models
{
    [Table("productos_tb")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El código del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El código no puede exceder los 100 caracteres")]
        [Display(Name = "Código del Producto")]
        [Column("codigo_producto")]
        public string CodigoProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre del Producto")]
        [Column("nombre_producto")]
        public string NombreProducto { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [Display(Name = "Descripción")]
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [StringLength(100, ErrorMessage = "La categoría no puede exceder los 100 caracteres")]
        [Display(Name = "Categoría")]
        [Column("categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio de compra es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio de compra debe estar entre 0.01 y 999,999.99")]
        [Display(Name = "Precio de Compra")]
        [Column("precio_compra")]
        [DataType(DataType.Currency)]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio de venta debe estar entre 0.01 y 999,999.99")]
        [Display(Name = "Precio de Venta")]
        [Column("precio_venta")]
        [DataType(DataType.Currency)]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "El impuesto es obligatorio")]
        [Range(0, 100, ErrorMessage = "El impuesto debe estar entre 0 y 100")]
        [Display(Name = "Impuesto (%)")]
        [Column("impuesto")]
        public decimal Impuesto { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20)]
        [Display(Name = "Estado")]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        // Propiedades calculadas (no mapeadas)
        [NotMapped]
        [Display(Name = "Margen de Ganancia")]
        public decimal MargenGanancia => PrecioVenta > 0 ? ((PrecioVenta - PrecioCompra) / PrecioVenta) * 100 : 0;

        [NotMapped]
        [Display(Name = "Utilidad")]
        public decimal Utilidad => PrecioVenta - PrecioCompra;

        [NotMapped]
        [Display(Name = "Precio con Impuesto")]
        public decimal PrecioConImpuesto => PrecioVenta + (PrecioVenta * (Impuesto / 100));

        // Relaciones con las tablas
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}