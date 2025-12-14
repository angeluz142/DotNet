
using ResultPattern.Domain.Entities;

namespace ResultPattern.Domain.Repositories
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> GetAllAsync(CancellationToken ct = default);
        Task<Proveedor?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Proveedor?> GetByNombreAsync(string nombre, CancellationToken ct = default);
        Task<Proveedor> AddAsync(Proveedor entity, CancellationToken ct = default);
        Task UpdateAsync(Proveedor entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
