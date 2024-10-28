using AccountMgt.Models.Models;
using AccountMgt.Application.DTOs;

namespace AccountMgt.Application.Abstraction.Services
{
    public interface IReportGenerator
    {
        Task<List<DetailedTransactionReport>> GetDetailedTransactionReportAsync(DateTime startDate, DateTime endDate);
    }
}
