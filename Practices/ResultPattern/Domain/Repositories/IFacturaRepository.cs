using ResultPattern.Domain.Entities;

namespace ResultPattern.Domain.Repositories
{
    public interface IFacturaRepository
    {
        Task<List<Factura>> GetAllAsync(CancellationToken ct = default);
        Task<Factura?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Factura> AddAsync(Factura entity, CancellationToken ct = default);
        Task UpdateAsync(Factura entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
