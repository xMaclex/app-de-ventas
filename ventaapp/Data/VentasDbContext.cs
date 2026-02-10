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
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //configuracion de cada tabla

        //Paises
        modelBuilder.Entity<Pais>()
        .HasKey(p => p.IdPais);

        //Ciudades
        modelBuilder.Entity<Ciudad>()
        .HasKey(c => c.IdCiudad);

        modelBuilder.Entity<Ciudad>()
            .HasOne(c => c.Pais)
            .WithMany(p => p.Ciudades)
            .HasForeignKey(c => c.IdPais);

        //clientes

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
