namespace ventaapp.Models;

public class Clientes
{
    public int IdCliente { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string TipoDocumento { get; set; } = string.Empty; 
    public string NumeroDocumento { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;

    // relaciones con las tablas
    
   public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}