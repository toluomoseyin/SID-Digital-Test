using AccountMgt.Application.Commands;
using AccountMgt.Application.DTOs;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Generate-report")]
        public async Task<IActionResult> GenerateReport(GenerateReportDTO model)
        {
            var pdfReport = await _mediator.Send(new GenerateReportCommand(model));

            return File(pdfReport.Data.File, "application/pdf", "Report.pdf");
        }
    }
}
