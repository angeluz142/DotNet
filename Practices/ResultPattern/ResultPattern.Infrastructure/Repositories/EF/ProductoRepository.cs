using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;

namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDbContext db) : base(db)
        {
        }
    }
}
