namespace ResultPattern.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Relación: cada producto pertenece a un proveedor
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;


    }
}
