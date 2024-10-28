using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountMgt.Infrastructure.Database.Repositories
{
    public class LinqReportGeneratorRepository : IReportGenerator
    {
        private readonly AppDbContext _context;

        public LinqReportGeneratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetailedTransactionReport>>
            GetDetailedTransactionReportAsync(
            DateTime startDate, DateTime endDate)
        {
            var report = await _context.Set<Transaction>()
                .Include(t => t.User)
                .Include(t => t.Merchant)
                .Include(t => t.Currency)
                .Include(t => t.TransactionType)
                .Include(t => t.Status)
                .Include(t => t.Fees)
                .Include(t => t.AuditTrails)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .GroupBy(t => new
                {
                    t.User.Name,
                    MerchantName = t.Merchant.Name,
                    t.Currency.Code,
                    t.TransactionType.Type,
                    TimeInterval = new
                    {
                        Year = t.TransactionDate.Year,
                        Month = t.TransactionDate.Month
                    }
                })
                .Select(g => new DetailedTransactionReport
                {
                    UserName = g.Key.Name,
                    MerchantName = g.Key.Name,
                    CurrencyCode = g.Key.Code,
                    TransactionType = g.Key.Type,
                    TotalAmount = g.Sum(t => t.Amount),
                    TransactionCount = g.Count(),
                    AverageAmount = g.Average(t => t.Amount),
                    TotalFees = g.Sum(t => t.Fees.Sum(f => f.Amount)),
                    LatestChangeDescription = g.SelectMany(t => t.AuditTrails)
                                                .OrderByDescending(a => a.ChangeDate)
                                                .Select(a => a.ChangeDescription)
                                                .FirstOrDefault(),
                    LastTransactionDate = g.Max(t => t.TransactionDate),
                    TimeInterval = $"{g.Key.TimeInterval.Year}-{g.Key.TimeInterval.Month:D2}"
                })
                .OrderByDescending(r => r.LastTransactionDate)
                .ToListAsync();

            return report;
        }


    }
}
