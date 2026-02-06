using Microsoft.EntityFrameworkCore;
using ventaapp.Models;

namespace ventaapp.Data;

public class VentasDbContext : DbContext
{
    public VentasDbContext(DbContextOptions<VentasDbContext> options) : base(options)
    {
    }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; } 
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Factura> Facturas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //configuracion de cada tabla

        //clientes

        modelBuilder.Entity<Clientes>()
        .HasKey(c => c.IdCliente);

        //productos
        modelBuilder.Entity<Producto>()
        .HasKey(p => p.IdProducto);

        //ventas
        modelBuilder.Entity<Venta>()
            .HasKey(v => v.IdVenta);
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.IdCliente);

        //Facturas
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
    }
    
}