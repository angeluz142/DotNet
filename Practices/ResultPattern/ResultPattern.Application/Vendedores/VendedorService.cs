using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Vendedores;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;

namespace ResultPattern.Application.Vendedores
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _repo;

        public VendedorService(IVendedorRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<VendedorDto>> CreateAsync(CreateVendedorRequest request, CancellationToken ct = default)
        {
            
            var vendedor = await _repo.GetByEmailAsync(request.Email, ct);
            if (vendedor is not null) return Result<VendedorDto>.BadRequest("Vendedor ya registrado");

            var entity = new Vendedor
            {
                Email = request.Email,
                Nombre = request.Nombre
            };

            var created = await _repo.AddAsync(entity, ct);
            var dto = new VendedorDto(created.Id, created.Nombre,created.Email);
            return Result<VendedorDto>.Created(dto);
        }

        public async Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<object>.NotFound("Producto no encontrado");

            await _repo.DeleteAsync(id, ct);
            return Result<object>.Ok("Borrado exitoso");
        }

        public async Task<Result<List<VendedorDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            var dtos = entities.Select(x => new VendedorDto(x.Id, x.Nombre, x.Email)).ToList();

            return Result<List<VendedorDto>>.Ok(dtos);
        }

        public async Task<Result<VendedorDto>> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var e = await _repo.GetByEmailAsync(email, ct);
            if (e is null) return Result<VendedorDto>.NotFound("Email del vendedor no encontrado", null);

            var dto = new VendedorDto(e.Id, e.Nombre, e.Email);
            return Result<VendedorDto>.Ok(dto);
        }

        public async Task<Result<VendedorDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return Result<VendedorDto>.NotFound("Vendedor no encontrado", null);

            var dto = new VendedorDto(e.Id, e.Nombre, e.Email);
            return Result<VendedorDto>.Ok(dto);
        }

        public async Task<Result<VendedorDto>> UpdateAsync(int id, UpdateVendedorRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<VendedorDto>.NotFound("Vendedor no encontrado");          

            entity.Nombre = request.Nombre;
            entity.Email = request.Email;           

            await _repo.UpdateAsync(entity, ct);

            var dto = new VendedorDto(entity.Id, entity.Nombre, entity.Email);
            return Result<VendedorDto>.Ok(dto);
        }
    }
}
