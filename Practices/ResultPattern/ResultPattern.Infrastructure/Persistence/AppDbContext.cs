using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;

namespace ResultPattern.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Vendedor> Vendedores => Set<Vendedor>();
        public DbSet<Factura> Facturas => Set<Factura>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Cliente -> Facturas
            //modelBuilder.Entity<Cliente>()
            //    .HasMany(c => c.Facturas)
            //    .WithOne(f => f.Cliente)
            //    .HasForeignKey(f => f.ClienteId);
            ////.OnDelete(DeleteBehavior.Cascade);

            //// Vendedor -> Facturas
            //modelBuilder.Entity<Vendedor>()
            //    .HasMany(v => v.Facturas)
            //    .WithOne(f => f.Vendedor)
            //    .HasForeignKey(f => f.VendedorId);
            ////.OnDelete(DeleteBehavior.Restrict);

            //// Proveedor -> Productos
            //modelBuilder.Entity<Proveedor>()
            //    .HasMany(p => p.Productos)
            //    .WithOne(pr => pr.Proveedor)
            //    .HasForeignKey(pr => pr.ProveedorId);
            //    //.OnDelete(DeleteBehavior.Cascade);

            //// Producto configuración
            //modelBuilder.Entity<Producto>(b =>
            //{
            //    b.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
            //    b.Property(p => p.Precio).HasColumnType("decimal(18,2)");
            //});

            //// Factura configuración
            //modelBuilder.Entity<Factura>(b =>
            //{
            //    b.Property(f => f.Total).HasColumnType("decimal(18,2)");
            //});


            modelBuilder.Entity<Cliente>(b =>
            {
                b.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.Dni).IsRequired();
                b.Property(x => x.Telefono).HasMaxLength(50);

                b.HasMany(c => c.Facturas)
                 .WithOne(f => f.Cliente)
                 .HasForeignKey(f => f.ClienteId);

                b.HasIndex(c => c.Dni)
                .IsUnique();
                 //.OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Vendedor>(b =>
            {
                b.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);

                b.HasMany(v => v.Facturas)
                 .WithOne(f => f.Vendedor)
                 .HasForeignKey(f => f.VendedorId);
                 //.OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Proveedor>(b =>
            {
                b.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
                b.Property(x => x.Contacto).HasMaxLength(200);

                b.HasMany(p => p.Productos)
                 .WithOne(pr => pr.Proveedor)
                 .HasForeignKey(pr => pr.ProveedorId);
                 //.OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Producto>(b =>
            {
                b.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
                b.Property(p => p.Precio).HasColumnType("decimal(18,2)");
                b.HasIndex(p => p.Nombre);
            });

            modelBuilder.Entity<Factura>(b =>
            {
                b.Property(f => f.Total).HasColumnType("decimal(18,2)");
                b.Property(f => f.Fecha).HasConversion(
                    v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });


        }

    }
}
