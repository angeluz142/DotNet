using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Clientes;

namespace ResultPattern.Application.Clientes
{
    public interface IClienteService
    {
        Task<Result<List<ClienteDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<ClienteDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<ClienteDto>> GetByDniAsync(int dni, CancellationToken ct = default);
        Task<Result<ClienteDto>> CreateAsync(CreateClienteRequest request, CancellationToken ct = default);
        Task<Result<ClienteDto>> UpdateAsync(int id, UpdateClienteRequest request, CancellationToken ct = default);
        Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default);
    }
}
