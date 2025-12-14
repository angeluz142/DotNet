using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Application.Dtos.Vendedores
{
    public record VendedorDto(int Id, string Nombre, string Email);
    public record CreateVendedorRequest(string Nombre, string Email);
    public record UpdateVendedorRequest(string Nombre, string Email);

}
