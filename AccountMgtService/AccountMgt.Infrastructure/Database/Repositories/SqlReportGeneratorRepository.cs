using AccountMgt.Models.Models;
using AccountMgt.Application.Abstraction.Services;
using Microsoft.EntityFrameworkCore;

namespace AccountMgt.Infrastructure.Database.Repositories
{
    public class SqlReportGeneratorRepository : IReportGenerator
    {
        private readonly AppDbContext _context;

        public SqlReportGeneratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetailedTransactionReport>>
            GetDetailedTransactionReportAsync(
            DateTime startDate, DateTime endDate)
        {
            return await _context.DetailedTransactionReports
                .FromSqlInterpolated($@"
                            SELECT 
                                u.Name AS UserName,
                                m.Name AS MerchantName,
                                c.Code AS CurrencyCode,
                                tt.Type AS TransactionType,
                                SUM(t.Amount) AS TotalAmount,
                                COUNT(*) AS TransactionCount,
                                AVG(t.Amount) AS AverageAmount,
                                SUM(f.Amount) AS TotalFees,
                                (SELECT TOP 1 a.ChangeDescription 
                                    FROM AuditTrails a 
                                    WHERE a.TransactionId = t.Id 
                                    ORDER BY a.ChangeDate DESC) AS LatestChangeDescription,
                                MAX(t.TransactionDate) AS LastTransactionDate,
                                CONCAT(YEAR(t.TransactionDate), '-', FORMAT(MONTH(t.TransactionDate), 'D2')) AS TimeInterval
                            FROM 
                                Transactions t
                            JOIN 
                                Users u ON t.UserId = u.Id
                            JOIN 
                                Merchants m ON t.MerchantId = m.Id
                            JOIN 
                                Currencies c ON t.CurrencyId = c.Id
                            JOIN 
                                TransactionTypes tt ON t.TransactionTypeId = tt.Id
                            LEFT JOIN 
                                Fees f ON t.Id = f.TransactionId
                            WHERE 
                                t.TransactionDate >= {startDate} AND t.TransactionDate <= {endDate}
                            GROUP BY 
                                u.Name, m.Name, c.Code, tt.Type
                ").ToListAsync();
        }

    }
}
