using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ventaapp.Models;

[Table("secuencias_ncf_tb")]
public class SecuenciaNcf
{
    [Key]
    [Column("id_secuencia")]
    public int IdSecuencia { get; set; }

    [Required(ErrorMessage = "El tipo de comprobante es obligatorio")]
    [StringLength(3)]
    [Display(Name = "Tipo de Comprobante")]
    [Column("tipo_comprobante")]
    public string TipoComprobante { get; set; } = string.Empty;

    [Display(Name = "Comprobante Fiscal")]
    [Column("numero_comprobante")]
    public int NumeroComprobante { get; set; }

    [Required(ErrorMessage = "El número inicial es obligatorio")]
    [Display(Name = "Número Inicial")]
    [Column("numero_inicial")]
    public int NumeroInicial { get; set; }

    [Required(ErrorMessage = "El número final es obligatorio")]
    [Display(Name = "Número Final")]
    [Column("numero_final")]
    public int NumeroFinal { get; set; }

    
    [Display(Name = "Número Actual")]
    [Column("numero_actual")]
    public int NumeroActual { get; set; }

    [Display(Name = "Ultimo comprobante")]
    [Column("numero_ultimo")]
    public int UlitmoNumero { get; set; }

    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
    [Display(Name = "Fecha de Vencimiento")]
    [Column("fecha_vencimiento")]
    [DataType(DataType.Date)]
    public DateTime FechaVencimiento { get; set; }

    [Display(Name = "Activa")]
    [Column("activa")]
    public bool Activa { get; set; } = true;


      public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
      public ICollection<Venta> Ventas { get; set; } = new List<Venta>();

    // ─── Propiedades calculadas ───────────────────────────────────────────────

    [NotMapped]
    public long Disponibles => NumeroFinal - NumeroActual;

    [NotMapped]
    public long TotalSecuencia => NumeroFinal - NumeroInicial;

    [NotMapped]
    public long Utilizados => NumeroActual - NumeroInicial;

    [NotMapped]
    public decimal PorcentajeUsado =>
        TotalSecuencia == 0 ? 0 :
        Math.Round((decimal)Utilizados / TotalSecuencia * 100, 1);

    [NotMapped]
    public bool CercaDeAgotarse => Disponibles > 0 && Disponibles < 100;

    [NotMapped]
    public bool Agotada => NumeroActual > NumeroFinal;

    [NotMapped]
    public bool Vencida => DateTime.Today > FechaVencimiento;

    [NotMapped]
    public string Descripcion => TipoComprobante switch
    {
        "B01" => "B01 - Crédito Fiscal",
        "B02" => "B02 - Consumidor Final",
        "B14" => "B14 - Régimen Especial",
        "B15" => "B15 - Gubernamental",
        _     => TipoComprobante
    };

    [NotMapped]
    public string EstadoVisual
    {
        get
        {
            if (!Activa)         return "Inactiva";
            if (Vencida)         return "Vencida";
            if (Agotada)         return "Agotada";
            if (CercaDeAgotarse) return "Por Agotar";
            return "Activa";
        }
    }

    [NotMapped]
    public string BadgeClass => EstadoVisual switch
    {
        "Activa"     => "bg-success",
        "Por Agotar" => "bg-warning text-dark",
        "Agotada"    => "bg-danger",
        "Vencida"    => "bg-danger",
        _            => "bg-secondary"
    };

    /// <summary>
    /// Genera el próximo NCF formateado (ej: "B020000000001") e incrementa
    /// el contador. Retorna null si la secuencia está agotada, vencida o inactiva.
    /// Llamar SaveChangesAsync() después para persistir el nuevo NumeroActual.
    /// </summary>
    public string? GenerarProximoNcf()
    {
        if (!Activa || Vencida || Agotada) return null;
        string ncf = $"{TipoComprobante}{NumeroActual:D10}";
        NumeroActual++;
        return ncf;
    }
}