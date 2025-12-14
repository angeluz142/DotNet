using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Productos;

namespace ResultPattern.Application.Productos
{
    public interface IProductoService
    {
        Task<Result<List<ProductoDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<ProductoDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<ProductoDto>> CreateAsync(CreateProductoRequest request, CancellationToken ct = default);
        Task<Result<ProductoDto>> UpdateAsync(int id, UpdateProductoRequest request, CancellationToken ct = default);
        Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default);
    }

}
