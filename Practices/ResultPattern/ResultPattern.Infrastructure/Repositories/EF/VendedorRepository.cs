using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Vendedor?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _db.Vendedores.FirstOrDefaultAsync(x=> x.Email == email,ct);
        }
    }
}
