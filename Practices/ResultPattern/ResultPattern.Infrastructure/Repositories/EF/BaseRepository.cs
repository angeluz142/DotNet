using Microsoft.EntityFrameworkCore;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;
using ResultPattern.Infrastructure.Persistence;


namespace ResultPattern.Infrastructure.Repositories.EF
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> _set;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            _set = db.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
       => await _set.AsNoTracking().ToListAsync(ct);

        public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _set.FindAsync(new object[] { id }, ct);

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _set.FindAsync(new object[] { id }, ct);
            if (entity is null) return;
            _set.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

    }

    //public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository { public ClienteRepository(AppDbContext db) : base(db) { } }
    //public class ProveedorRepository : BaseRepository<Proveedor>, IProveedorRepository { public ProveedorRepository(AppDbContext db) : base(db) { } }

    //public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    //{
    //    public ProductoRepository(AppDbContext db) : base(db) { }

    //    public new async Task<List<Producto>> GetAllAsync(CancellationToken ct = default)
    //        => await _db.Productos.Include(p => p.Proveedor).AsNoTracking().ToListAsync(ct);

    //    public new async Task<Producto?> GetByIdAsync(int id, CancellationToken ct = default)
    //        => await _db.Productos.Include(p => p.Proveedor).FirstOrDefaultAsync(p => p.Id == id, ct);
    //}

    //public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository { public VendedorRepository(AppDbContext db) : base(db) { } }

    //public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
    //{
    //    public FacturaRepository(AppDbContext db) : base(db) { }

    //    public new async Task<List<Factura>> GetAllAsync(CancellationToken ct = default)
    //        => await _db.Facturas.Include(f => f.Cliente).Include(f => f.Vendedor).AsNoTracking().ToListAsync(ct);

    //    public new async Task<Factura?> GetByIdAsync(int id, CancellationToken ct = default)
    //        => await _db.Facturas.Include(f => f.Cliente).Include(f => f.Vendedor).FirstOrDefaultAsync(f => f.Id == id, ct);
    //}

}
