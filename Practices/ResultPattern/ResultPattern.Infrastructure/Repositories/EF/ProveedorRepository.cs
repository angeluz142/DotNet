using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class ProveedorRepository : BaseRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Proveedor?> GetByNombreAsync(string nombre, CancellationToken ct = default)
        {
            return await _db.Proveedores.FirstOrDefaultAsync(x => x.Nombre == nombre, ct);
        }
    }
}
