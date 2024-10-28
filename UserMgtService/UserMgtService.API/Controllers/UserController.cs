using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMgtService.Application.Commands;
using UserMgtService.Application.DTOs;

namespace UserMgtService.API.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for sending commands.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration details.</param>
        /// <returns>A response indicating success or failure.</returns>
        /// <response code="200">Returns the registration success response.</response>
        /// <response code="400">If the registration details are invalid.</response>
        /// <response code="429">If the request exceeds the rate limit.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegistrationDTO model)
        {
            return Ok(await _mediator.Send(new RegistrationCommand(model)));
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>A token if successful.</returns>
        /// <response code="200">Returns the authentication token.</response>
        /// <response code="400">If the login details are invalid.</response>
        /// <response code="429">If the request exceeds the rate limit.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse<LoggedInUserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            return Ok(await _mediator.Send(new LoginCommand(model)));
        }

        /// <summary>
        /// Changes the role of a user.
        /// </summary>
        /// <param name="model">The change role details.</param>
        /// <returns>A response indicating success or failure.</returns>
        /// <response code="200">Returns the role change success response.</response>
        /// <response code="400">If the role change details are invalid.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="429">If the request exceeds the rate limit.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPost("change-role")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeRole(ChangeRoleDTO model)
        {
            return Ok(await _mediator.Send(new ChangeRoleCommand(model)));
        }
    }
}
