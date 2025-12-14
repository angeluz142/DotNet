using ResultPattern.Api.Filters;
using ResultPattern.Application.Clientes;
using ResultPattern.Application.Facturas;
using ResultPattern.Application.Productos;
using ResultPattern.Application.Proveedores;
using ResultPattern.Infrastructure;
using ResultPattern.Infrastructure.Persistence;
using ResultPattern.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);

// Services de aplicación
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
// Crea y registra IClienteService, IProveedorService, IVendedorService análogos

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ResultFilter>();
    opt.Filters.Add<GlobalExceptionFilter>();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
