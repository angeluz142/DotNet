namespace ResultPattern.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Relación: un cliente puede tener muchas facturas
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    }
}
