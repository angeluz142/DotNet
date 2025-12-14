using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Facturas;
using ResultPattern.Domain.Entities;
using ResultPattern.Domain.Repositories;

namespace ResultPattern.Application.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repo;
        private readonly IClienteRepository _cliRepo;
        private readonly IVendedorRepository _venRepo;

        public FacturaService(IFacturaRepository repo, IClienteRepository cliRepo, IVendedorRepository venRepo)
        {
            _repo = repo;
            _cliRepo = cliRepo;
            _venRepo = venRepo;
        }

        public async Task<Result<List<FacturaDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllAsync(ct);
            var dtos = list.Select(f => new FacturaDto(
                f.Id, f.Fecha, f.Total, f.ClienteId, f.Cliente?.Nombre ?? string.Empty, f.VendedorId, f.Vendedor?.Nombre ?? string.Empty)).ToList();

            return Result<List<FacturaDto>>.Ok(dtos);
        }

        public async Task<Result<FacturaDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var f = await _repo.GetByIdAsync(id, ct);
            if (f is null) return Result<FacturaDto>.NotFound("Factura no encontrada");

            var dto = new FacturaDto(f.Id, f.Fecha, f.Total, f.ClienteId, f.Cliente?.Nombre ?? string.Empty, f.VendedorId, f.Vendedor?.Nombre ?? string.Empty);
            return Result<FacturaDto>.Ok(dto);
        }

        public async Task<Result<FacturaDto>> CreateAsync(CreateFacturaRequest request, CancellationToken ct = default)
        {
            if (request.Total < 0) return Result<FacturaDto>.BadRequest("Total inválido");

            var cliente = await _cliRepo.GetByIdAsync(request.ClienteId, ct);
            if (cliente is null) return Result<FacturaDto>.BadRequest("Cliente inválido");

            var vendedor = await _venRepo.GetByIdAsync(request.VendedorId, ct);
            if (vendedor is null) return Result<FacturaDto>.BadRequest("Vendedor inválido");

            var entity = new Factura
            {
                Fecha = request.Fecha == default ? DateTime.UtcNow : request.Fecha,
                Total = request.Total,
                ClienteId = request.ClienteId,
                VendedorId = request.VendedorId
            };

            var created = await _repo.AddAsync(entity, ct);
            var dto = new FacturaDto(created.Id, created.Fecha, created.Total, created.ClienteId, cliente.Nombre, created.VendedorId, vendedor.Nombre);
            return Result<FacturaDto>.Created(dto);
        }

        public async Task<Result<FacturaDto>> UpdateAsync(int id, UpdateFacturaRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<FacturaDto>.NotFound("Factura no encontrada");

            var cliente = await _cliRepo.GetByIdAsync(request.ClienteId, ct);
            if (cliente is null) return Result<FacturaDto>.BadRequest("Cliente inválido");

            var vendedor = await _venRepo.GetByIdAsync(request.VendedorId, ct);
            if (vendedor is null) return Result<FacturaDto>.BadRequest("Vendedor inválido");

            entity.Fecha = request.Fecha == default ? entity.Fecha : request.Fecha;
            entity.Total = request.Total;
            entity.ClienteId = request.ClienteId;
            entity.VendedorId = request.VendedorId;

            await _repo.UpdateAsync(entity, ct);
            var dto = new FacturaDto(entity.Id, entity.Fecha, entity.Total, entity.ClienteId, cliente.Nombre, entity.VendedorId, vendedor.Nombre);
            return Result<FacturaDto>.Ok(dto);
        }

        public async Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<object>.NotFound("Factura no encontrada");

            await _repo.DeleteAsync(id, ct);
            return Result<object>.Ok("Borrado exitoso");
        }

    }
}
