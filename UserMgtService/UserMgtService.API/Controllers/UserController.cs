using UserMgtService.Application.Commands;
using UserMgtService.Application.DTOs;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserMgtService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDTO model)
        {
            return Ok(await _mediator.Send(new RegistrationCommand(model)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            return Ok(await _mediator.Send(new LoginCommand(model)));
        }

        [Authorize]
        [HttpPost("change-role")]       
        public async Task<IActionResult> ChangeRole(ChangeRoleDTO model)
        {
            return Ok(await _mediator.Send(new ChangeRoleCommand(model)));
        }
    }
}
