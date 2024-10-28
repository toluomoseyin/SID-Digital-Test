using AccountMgt.Application.Commands;
using AccountMgt.Application.DTOs;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.API.Controllers
{
    /// <summary>
    /// Controller for managing transaction-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands.</param>
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Generates a PDF report based on the provided report criteria.
        /// </summary>
        /// <param name="model">The details required to generate the report.</param>
        /// <returns>A PDF file containing the report.</returns>
        /// <response code="200">Returns the generated PDF report.</response>
        /// <response code="400">If the report generation details are invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("Generate-report")]
        public async Task<IActionResult> GenerateReport(GenerateReportDTO model)
        {
            var pdfReport = await _mediator.Send(new GenerateReportCommand(model));

            return File(pdfReport.Data.File, "application/pdf", "Report.pdf");
        }
    }
}
