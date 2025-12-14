using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Productos;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;

namespace ResultPattern.Application.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;
        private readonly IProveedorRepository _provRepo;

        public ProductoService(IProductoRepository repo, IProveedorRepository provRepo)
        {
            _repo = repo;
            _provRepo = provRepo;
        }

        public async Task<Result<List<ProductoDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            var dtos = entities.Select(e => new ProductoDto(
                e.Id, e.Nombre, e.Descripcion, e.Precio, e.ProveedorId, e.Proveedor?.Nombre ?? string.Empty)).ToList();

            return Result<List<ProductoDto>>.Ok(dtos);
        }

        public async Task<Result<ProductoDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return Result<ProductoDto>.NotFound("Producto no encontrado", null);

            var dto = new ProductoDto(e.Id, e.Nombre, e.Descripcion, e.Precio, e.ProveedorId, e.Proveedor?.Nombre ?? string.Empty);
            return Result<ProductoDto>.Ok(dto);
        }

        public async Task<Result<ProductoDto>> CreateAsync(CreateProductoRequest request, CancellationToken ct = default)
        {
            if (request.Precio <= 0) return Result<ProductoDto>.BadRequest("El precio debe ser mayor a 0");

            var proveedor = await _provRepo.GetByIdAsync(request.ProveedorId, ct);
            if (proveedor is null) return Result<ProductoDto>.BadRequest("Proveedor inválido");

            var entity = new Producto
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                ProveedorId = request.ProveedorId
            };

            var created = await _repo.AddAsync(entity, ct);
            var dto = new ProductoDto(created.Id, created.Nombre, created.Descripcion, created.Precio, created.ProveedorId, proveedor.Nombre);
            return Result<ProductoDto>.Created(dto);
        }

        public async Task<Result<ProductoDto>> UpdateAsync(int id, UpdateProductoRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<ProductoDto>.NotFound("Producto no encontrado");

            var proveedor = await _provRepo.GetByIdAsync(request.ProveedorId, ct);
            if (proveedor is null) return Result<ProductoDto>.BadRequest("Proveedor inválido");

            entity.Nombre = request.Nombre;
            entity.Descripcion = request.Descripcion;
            entity.Precio = request.Precio;
            entity.ProveedorId = request.ProveedorId;

            await _repo.UpdateAsync(entity, ct);

            var dto = new ProductoDto(entity.Id, entity.Nombre, entity.Descripcion, entity.Precio, entity.ProveedorId, proveedor.Nombre);
            return Result<ProductoDto>.Ok(dto);
        }

        public async Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<object>.NotFound("Producto no encontrado");

            await _repo.DeleteAsync(id, ct);
            //return Result<object>.NoContent();
            return Result<object>.Ok("Borrado exitoso");
        }

    }
}
