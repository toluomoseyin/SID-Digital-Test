using UserMgtService.Application.DTOs;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace UserMgtService.Application.Commands
{
    public sealed record class EmailConfirmationCommand(string userId,HttpRequest HttpRequest) : ICommand<AuthResponse>;
}
