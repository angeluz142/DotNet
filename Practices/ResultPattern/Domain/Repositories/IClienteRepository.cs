using ResultPattern.Domain.Entities;

namespace ResultPattern.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync(CancellationToken ct = default);
        Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Cliente?> GetByDniAsync(int dni, CancellationToken ct = default);
        Task<Cliente> AddAsync(Cliente entity, CancellationToken ct = default);
        Task UpdateAsync(Cliente entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
