using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
    {
        public FacturaRepository(AppDbContext db) : base(db)
        {
        }

        public override async Task<List<Factura>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Facturas
            .Include(f => f.Cliente)
            .Include(f => f.Vendedor)
            .AsNoTracking()
            .ToListAsync(ct);
        }
        public override async Task<Factura?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _db.Facturas
           .Include(f => f.Cliente)
           .Include(f => f.Vendedor)
           .FirstOrDefaultAsync(f => f.Id == id, ct);
        }

    }
}
