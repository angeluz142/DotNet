using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Domain.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        // Relación: cada factura pertenece a un cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // Relación: cada factura pertenece a un vendedor
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; } = null!;

    }
}
