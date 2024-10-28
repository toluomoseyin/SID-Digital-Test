using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Application.DTOs;
using Mediator;

namespace AccountMgt.Application.Commands.Handler
{
    public class GenerateReportCommandHandler
      : ICommandHandler<GenerateReportCommand, AuthResponse<GenerateReportResponse>>
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly IPdfReportService _pdfReportService;

        public GenerateReportCommandHandler(
            IReportGenerator reportGenerator,
            IPdfReportService pdfReportService)
        {
            _reportGenerator = reportGenerator;
            _pdfReportService = pdfReportService;
        }

        public async ValueTask<AuthResponse<GenerateReportResponse>> Handle
            (GenerateReportCommand command, CancellationToken cancellationToken)
        {
            var response = await _reportGenerator
                .GetDetailedTransactionReportAsync(
                command.Data.startDate, command.Data.endDate);

            var pdfResult = await _pdfReportService
                .GenerateTransactionReportPdfAsync(response);

            return AuthResponse<GenerateReportResponse>.Successful("Success", new GenerateReportResponse
            {
                File = pdfResult
            });
        }
    }

}
