
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Common
{
    public record ProgramActivityResponse(
        Guid Id,
        Guid IdProgram,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        string State
    );
}
