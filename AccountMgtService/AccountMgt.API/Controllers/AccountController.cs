using AccountMgt.Application.DTOs;
using AccountMgt.Application.Commands;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.API.Controllers
{
    /// <summary>
    /// Controller for managing user accounts.
    /// </summary>
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands.</param>
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="model">The data transfer object containing the account creation details.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// The result contains an <see cref="AuthResponse{T}"/> on success, or appropriate <see cref="ProblemDetails"/> for errors.
        /// </returns>
        /// <response code="200">Returns the created account information.</response>
        /// <response code="400">If the request is invalid or missing required fields.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse<CreateAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAccount(CreateAccountDTO model)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            return Ok(await _mediator.Send(new CreateAccountCommand(model, userId)));
        }
    }
}
