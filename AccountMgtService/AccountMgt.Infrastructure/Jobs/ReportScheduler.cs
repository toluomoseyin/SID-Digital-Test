using AccountMgt.Infrastructure.Database.Repositories;
using AccountMgt.Infrastructure.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AccountMgt.Infrastructure.Jobs
{
    public class ReportScheduler
    {

        private readonly IServiceProvider _serviceProvider;

        public ReportScheduler(
           IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;


        }


        public void ScheduleDailyReportGeneration()
        {
            var recurringJobManager = _serviceProvider.GetRequiredService<IRecurringJobManager>();

            // Schedule the recurring job
            recurringJobManager.AddOrUpdate(
                "DailyTransactionReport",
                () => GenerateAndStoreReport(),
                Cron.Daily);
        }

        public async Task GenerateAndStoreReport()
        {
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow;
            using var scope = _serviceProvider.CreateScope();

            var reportGenerator = scope
                .ServiceProvider
                .GetRequiredService<LinqReportGeneratorRepository>();

            var pdfReportService = scope
                .ServiceProvider
                .GetRequiredService<PdfReportService>();

            var reportData = await reportGenerator
                .GetDetailedTransactionReportAsync(startDate, endDate);

            var pdfData = await pdfReportService
                .GenerateTransactionReportPdfAsync(reportData);

            await SaveReportToFile(pdfData);
        }



        private async Task SaveReportToFile(byte[] pdfData)
        {
            var filePath = Path.Combine("Reports",
                $"TransactionReport_{DateTime.Now:yyyyMMdd}.pdf");

            await File.WriteAllBytesAsync(filePath, pdfData);
        }
    }
}
