using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Domain.Entities
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Relación: un vendedor puede emitir muchas facturas
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    }
}
