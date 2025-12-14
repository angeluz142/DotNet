namespace ResultPattern.Application.Dtos.Productos
{
    public record ProductoDto(int Id, string Nombre, string? Descripcion, decimal Precio, int ProveedorId, string ProveedorNombre);
    public record CreateProductoRequest(string Nombre, string? Descripcion, decimal Precio, int ProveedorId);
    public record UpdateProductoRequest(string Nombre, string? Descripcion, decimal Precio, int ProveedorId);

}
