using ResultPattern.Domain.Entities;

namespace ResultPattern.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync(CancellationToken ct = default);
        Task<Producto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Producto> AddAsync(Producto entity, CancellationToken ct = default);
        Task UpdateAsync(Producto entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
