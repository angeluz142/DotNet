namespace ResultPattern.Application.Dtos.Proveedores
{
    public record ProveedorDto(int Id, string Nombre, string Contacto);
    public record CreateProveedorRequest(string Nombre, string Contacto);
    public record UpdateProveedorRequest(string Nombre, string Contacto);

}
