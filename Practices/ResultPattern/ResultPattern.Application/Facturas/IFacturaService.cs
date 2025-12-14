using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Facturas;

namespace ResultPattern.Application.Facturas
{
    public interface IFacturaService
    {
        Task<Result<List<FacturaDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<FacturaDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<FacturaDto>> CreateAsync(CreateFacturaRequest request, CancellationToken ct = default);
        Task<Result<FacturaDto>> UpdateAsync(int id, UpdateFacturaRequest request, CancellationToken ct = default);
        Task<Result<object>> DeleteAsync(int id, CancellationToken ct = default);

    }
}
