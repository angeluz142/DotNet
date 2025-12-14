using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Proveedores;

namespace ResultPattern.Application.Proveedores
{
    public interface IProveedorService
    {
        Task<Result<List<ProveedorDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<ProveedorDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<ProveedorDto>> GetByNombreAsync(string nombre, CancellationToken ct = default);
        Task<Result<ProveedorDto>> CreateAsync(CreateProveedorRequest request, CancellationToken ct = default);
        Task<Result<ProveedorDto>> UpdateAsync(int id, UpdateProveedorRequest request, CancellationToken ct = default);
        Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default);
    }
}
