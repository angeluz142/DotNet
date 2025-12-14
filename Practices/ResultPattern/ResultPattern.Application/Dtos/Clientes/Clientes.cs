namespace ResultPattern.Application.Dtos.Clientes
{

    public record ClienteDto(int Id, string Nombre, string Email, string Telefono, int Dni);
    public record CreateClienteRequest(string Nombre, string Email, string Telefono, int Dni);
    public record UpdateClienteRequest(string Nombre, string Email, string Telefono, int Dni);


}
