using ResultPattern.Domain.Entities;

namespace ResultPattern.Domain.Repositories
{
    public interface IVendedorRepository
    {
        Task<List<Vendedor>> GetAllAsync(CancellationToken ct = default);
        Task<Vendedor?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<Vendedor?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Vendedor> AddAsync(Vendedor entity, CancellationToken ct = default);
        Task UpdateAsync(Vendedor entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
