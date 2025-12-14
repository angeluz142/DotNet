using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Proveedores;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;

namespace ResultPattern.Application.Proveedores
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repo;

        public ProveedorService(IProveedorRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<ProveedorDto>> CreateAsync(CreateProveedorRequest request, CancellationToken ct = default)
        {
            var vendedor = await _repo.GetByNombreAsync(request.Nombre, ct);
            if (vendedor is not null) return Result<ProveedorDto>.BadRequest("Proveedor ya registrado");

            var entity = new Proveedor
            {
                Nombre = request.Nombre,
                Contacto = request.Contacto
            };

            var created = await _repo.AddAsync(entity, ct);
            var dto = new ProveedorDto(created.Id, created.Nombre, created.Contacto);
            return Result<ProveedorDto>.Created(dto);
        }

        public async Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<object>.NotFound("Proveedor no encontrado");

            await _repo.DeleteAsync(id, ct);
            return Result<object>.Ok("Borrado exitoso");
        }

        public async Task<Result<List<ProveedorDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            var dtos = entities.Select(x => new ProveedorDto(x.Id, x.Nombre, x.Contacto)).ToList();

            return Result<List<ProveedorDto>>.Ok(dtos);
        }

        public async Task<Result<ProveedorDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return Result<ProveedorDto>.NotFound("Proveedor no encontrado", null);

            var dto = new ProveedorDto(e.Id, e.Nombre, e.Contacto);
            return Result<ProveedorDto>.Ok(dto);
        }

        public async Task<Result<ProveedorDto>> GetByNombreAsync(string nombre, CancellationToken ct = default)
        {
            var e = await _repo.GetByNombreAsync(nombre, ct);
            if (e is null) return Result<ProveedorDto>.NotFound("Proveedor no encontrado", null);

            var dto = new ProveedorDto(e.Id, e.Nombre, e.Contacto);
            return Result<ProveedorDto>.Ok(dto);
        }

        public async Task<Result<ProveedorDto>> UpdateAsync(int id, UpdateProveedorRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<ProveedorDto>.NotFound("Proveedor no encontrado");

            entity.Nombre = request.Nombre;
            entity.Contacto = request.Contacto;

            await _repo.UpdateAsync(entity, ct);

            var dto = new ProveedorDto(entity.Id, entity.Nombre, entity.Contacto);
            return Result<ProveedorDto>.Ok(dto);
        }
    }
}
