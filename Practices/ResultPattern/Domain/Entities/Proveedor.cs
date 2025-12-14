using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Domain.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;

        // Relación: un proveedor puede tener muchos productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }
}
