using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ventaapp.Models;

namespace ventaapp.Data;

public class VentasDbContext : IdentityDbContext<ApplicationUser>
{
    public VentasDbContext(DbContextOptions<VentasDbContext> options) : base(options)
    {
    }

    public DbSet<Clientes>     Clientes     { get; set; }
    public DbSet<Producto>     Productos    { get; set; }
    public DbSet<Venta>        Ventas       { get; set; }
    public DbSet<Factura>      Facturas     { get; set; }
    public DbSet<Pais>         Paises       { get; set; }
    public DbSet<Ciudad>       Ciudades     { get; set; }
    public DbSet<Usuarios>     Usuario      { get; set; }
    public DbSet<SecuenciaNcf> SecuenciaNcf { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Usuarios ──────────────────────────────────────────────────
        modelBuilder.Entity<Usuarios>()
            .HasKey(u => u.IdUsuario);

        // ── Paises ────────────────────────────────────────────────────
        modelBuilder.Entity<Pais>()
            .HasKey(p => p.IdPais);

        // ── Ciudades ──────────────────────────────────────────────────
        modelBuilder.Entity<Ciudad>()
            .HasKey(c => c.IdCiudad);

        modelBuilder.Entity<Ciudad>()
            .HasOne(c => c.Pais)
            .WithMany(p => p.Ciudades)
            .HasForeignKey(c => c.IdPais);

        // ── Clientes ──────────────────────────────────────────────────
        modelBuilder.Entity<Clientes>()
            .HasKey(c => c.IdCliente);

        modelBuilder.Entity<Clientes>()
            .HasOne(c => c.Pais)
            .WithMany(p => p.Clientes)
            .HasForeignKey(c => c.IdPais);

        modelBuilder.Entity<Clientes>()
            .HasOne(c => c.Ciudad)
            .WithMany(ci => ci.Clientes)
            .HasForeignKey(c => c.IdCiudad);

        // ── Productos ─────────────────────────────────────────────────
        modelBuilder.Entity<Producto>()
            .HasKey(p => p.IdProducto);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Stock)
            .HasDefaultValue(0);

        // ── Secuencias NCF ────────────────────────────────────────────
        modelBuilder.Entity<SecuenciaNcf>()
            .HasKey(s => s.IdSecuencia);

        modelBuilder.Entity<SecuenciaNcf>()
            .HasIndex(s => new { s.TipoComprobante, s.Activa });

        // ── Ventas ────────────────────────────────────────────────────
        modelBuilder.Entity<Venta>()
            .HasKey(v => v.IdVenta);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.IdCliente);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Usuario)
            .WithMany(u => u.Ventas)
            .HasForeignKey(v => v.IdUsuario);

        // FK a la PK real de SecuenciaNcf
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.SecuenciaNcf)
            .WithMany(s => s.Ventas)
            .HasForeignKey(v => v.IdSecuenciaNcf);

        // ── Facturas ──────────────────────────────────────────────────
        modelBuilder.Entity<Factura>()
            .HasKey(f => f.IdFactura);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Venta)
            .WithMany(v => v.Facturas)
            .HasForeignKey(f => f.IdVenta);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Cliente)
            .WithMany(c => c.Facturas)
            .HasForeignKey(f => f.IdCliente);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Producto)
            .WithMany(p => p.Facturas)
            .HasForeignKey(f => f.IdProducto);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Usuario)
            .WithMany(u => u.Facturas)
            .HasForeignKey(f => f.IdUsuario);

        // FK a la PK real de SecuenciaNcf
        modelBuilder.Entity<Factura>()
            .HasOne(f => f.SecuenciaNcf)
            .WithMany(s => s.Facturas)
            .HasForeignKey(f => f.IdSecuenciaNcf);
    }
}