using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Productos;
using ResultPattern.Application.Dtos.Vendedores;

namespace ResultPattern.Application.Vendedores
{
    public interface IVendedorService
    {
        Task<Result<List<VendedorDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<VendedorDto>> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<VendedorDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<VendedorDto>> CreateAsync(CreateVendedorRequest request, CancellationToken ct = default);
        Task<Result<VendedorDto>> UpdateAsync(int id, UpdateVendedorRequest request, CancellationToken ct = default);
        Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default);
    }
}
