using AccountMgt.Models.Models;

namespace AccountMgt.Application.Abstraction.Services
{
    public interface IPdfReportService
    {
        Task<byte[]?> GenerateTransactionReportPdfAsync(List<DetailedTransactionReport> reportData);
    }
}
