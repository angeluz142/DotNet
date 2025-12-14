using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using ResultPattern.Domain.Entities;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Seeders
{
    public class DbSeeder
    {
        //public static async Task SeedAsync(AppDbContext db, CancellationToken ct = default)
        //{
        //    await db.Database.MigrateAsync(ct);

        //    if (await db.Clientes.AnyAsync(ct)) return;

        //    var clienteFaker = new Faker<Cliente>()
        //        .RuleFor(c => c.Nombre, f => f.Name.FullName())
        //        .RuleFor(c => c.Email, f => f.Internet.Email())
        //        .RuleFor(c => c.Telefono, f => f.Phone.PhoneNumber());

        //    var proveedorFaker = new Faker<Proveedor>()
        //        .RuleFor(p => p.Nombre, f => f.Company.CompanyName())
        //        .RuleFor(p => p.Contacto, f => f.Internet.Email());

        //    var vendedorFaker = new Faker<Vendedor>()
        //        .RuleFor(v => v.Nombre, f => f.Name.FullName())
        //        .RuleFor(v => v.Email, f => f.Internet.Email());

        //    var clientes = clienteFaker.Generate(20);
        //    var proveedores = proveedorFaker.Generate(10);
        //    var vendedores = vendedorFaker.Generate(5);

        //    db.Clientes.AddRange(clientes);
        //    db.Proveedores.AddRange(proveedores);
        //    db.Vendedores.AddRange(vendedores);
        //    await db.SaveChangesAsync(ct);

        //    var productoFaker = new Faker<Producto>()
        //        .RuleFor(p => p.Nombre, f => f.Commerce.ProductName())
        //        .RuleFor(p => p.Descripcion, f => f.Commerce.ProductDescription())
        //        .RuleFor(p => p.Precio, f => decimal.Parse(f.Commerce.Price()))
        //        .RuleFor(p => p.ProveedorId, f => f.PickRandom(proveedores).Id);

        //    var productos = productoFaker.Generate(50);
        //    db.Productos.AddRange(productos);
        //    await db.SaveChangesAsync(ct);

        //    var facturaFaker = new Faker<Factura>()
        //        .RuleFor(f => f.Fecha, f => f.Date.Recent(30))
        //        .RuleFor(f => f.Total, f => decimal.Parse(f.Commerce.Price()))
        //        .RuleFor(f => f.ClienteId, f => f.PickRandom(clientes).Id)
        //        .RuleFor(f => f.VendedorId, f => f.PickRandom(vendedores).Id);

        //    var facturas = facturaFaker.Generate(30);
        //    db.Facturas.AddRange(facturas);
        //    await db.SaveChangesAsync(ct);
        //}

        public static async Task SeedAsync(AppDbContext db, CancellationToken ct = default)
        {
            await db.Database.MigrateAsync(ct);

            if (await db.Clientes.AnyAsync(ct)) return;
            var usedDnis = new HashSet<int>();

            var clientes = new Faker<Cliente>("es")
                .RuleFor(c => c.Nombre, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Dni, f =>
                {
                    int dni;
                    do
                    {
                        dni = f.Random.Int(20000000, 50000000);
                    } while (!usedDnis.Add(dni));
                    return dni;
                })
                .RuleFor(c => c.Telefono, f => f.Phone.PhoneNumber())
                .Generate(20);

            var proveedores = new Faker<Proveedor>("es")
                .RuleFor(p => p.Nombre, f => f.Company.CompanyName())
                .RuleFor(p => p.Contacto, f => f.Internet.Email())
                .Generate(10);

            var vendedores = new Faker<Vendedor>("es")
                .RuleFor(v => v.Nombre, f => f.Name.FullName())
                .RuleFor(v => v.Email, f => f.Internet.Email())
                .Generate(5);

            db.Clientes.AddRange(clientes);
            db.Proveedores.AddRange(proveedores);
            db.Vendedores.AddRange(vendedores);
            await db.SaveChangesAsync(ct);

            var productos = new Faker<Producto>("es")
                .RuleFor(p => p.Nombre, f => f.Commerce.ProductName())
                .RuleFor(p => p.Descripcion, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Precio, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.ProveedorId, f => f.PickRandom(proveedores).Id)
                .Generate(50);

            db.Productos.AddRange(productos);
            await db.SaveChangesAsync(ct);

            var facturas = new Faker<Factura>("es")
                .RuleFor(f => f.Fecha, f => DateTime.SpecifyKind(f.Date.Recent(30), DateTimeKind.Utc))
                .RuleFor(f => f.Total, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(f => f.ClienteId, f => f.PickRandom(clientes).Id)
                .RuleFor(f => f.VendedorId, f => f.PickRandom(vendedores).Id)
                .Generate(30);

            db.Facturas.AddRange(facturas);
            await db.SaveChangesAsync(ct);
        }


    }
}
