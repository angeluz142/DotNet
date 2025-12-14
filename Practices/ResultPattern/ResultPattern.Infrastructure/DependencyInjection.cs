using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;
using ResultPattern.Infrastructure.Repositories.EF;

namespace ResultPattern.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("Default") ?? "Data Source=cleanapi.db";

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connection));

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();

            return services;
        }

    }
}
