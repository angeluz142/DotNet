using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Application.Dtos.Facturas
{
    public record FacturaDto(int Id, DateTime Fecha, decimal Total, int ClienteId, string ClienteNombre, int VendedorId, string VendedorNombre);
    public record CreateFacturaRequest(DateTime Fecha, decimal Total, int ClienteId, int VendedorId);
    public record UpdateFacturaRequest(DateTime Fecha, decimal Total, int ClienteId, int VendedorId);

}
