using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Clientes;
using ResultPattern.Application.Dtos.Vendedores;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;

namespace ResultPattern.Application.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<ClienteDto>> CreateAsync(CreateClienteRequest request, CancellationToken ct = default)
        {
            var vendedor = await _repo.GetByDniAsync(request.Dni, ct);
            if (vendedor is not null) return Result<ClienteDto>.BadRequest("Vendedor ya registrado");

            var entity = new Cliente
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Dni = request.Dni,
                Telefono = request.Telefono
            };

            var created = await _repo.AddAsync(entity, ct);
            var dto = new ClienteDto(created.Id, created.Nombre, created.Email, created.Telefono, created.Dni);
            return Result<ClienteDto>.Created(dto);
        }

        public async Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<object>.NotFound("Cliente no encontrado");

            await _repo.DeleteAsync(id, ct);
            return Result<object>.Ok("Borrado exitoso");
        }

        public async Task<Result<List<ClienteDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            var dtos = entities.Select(x => new ClienteDto(x.Id, x.Nombre, x.Email, x.Telefono, x.Dni)).ToList();

            return Result<List<ClienteDto>>.Ok(dtos);
        }

        public async Task<Result<ClienteDto>> GetByDniAsync(int dni, CancellationToken ct = default)
        {
            var e = await _repo.GetByDniAsync(dni, ct);
            if (e is null) return Result<ClienteDto>.NotFound("Dni del cliente no encontrado", null);

            var dto = new ClienteDto(e.Id, e.Nombre, e.Email, e.Telefono, e.Dni);
            return Result<ClienteDto>.Ok(dto);
        }

        public async Task<Result<ClienteDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return Result<ClienteDto>.NotFound("Cliente no encontrado", null);

            var dto = new ClienteDto(e.Id, e.Nombre, e.Email, e.Telefono, e.Dni);
            return Result<ClienteDto>.Ok(dto);
        }

        public async Task<Result<ClienteDto>> UpdateAsync(int id, UpdateClienteRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<ClienteDto>.NotFound("Cliente no encontrado");

            entity.Nombre = request.Nombre;
            entity.Email = request.Email;

            await _repo.UpdateAsync(entity, ct);

            var dto = new ClienteDto(entity.Id, entity.Nombre, entity.Email, entity.Telefono, entity.Dni);
            return Result<ClienteDto>.Ok(dto);
        }
    }
}
