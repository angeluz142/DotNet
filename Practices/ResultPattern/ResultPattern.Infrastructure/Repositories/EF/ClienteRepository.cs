using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Cliente?> GetByDniAsync(int dni, CancellationToken ct = default)
        {
            return await _db.Clientes.FirstOrDefaultAsync(x => x.Dni == dni, ct);
        }
    }
}
