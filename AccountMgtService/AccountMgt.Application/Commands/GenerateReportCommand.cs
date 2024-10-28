using AccountMgt.Application.DTOs;
using Mediator;

namespace AccountMgt.Application.Commands
{
    public sealed record class GenerateReportCommand(GenerateReportDTO Data) : ICommand<AuthResponse<GenerateReportResponse>>;
}
