
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Update
{
    public record UpdateProgramActivityCommand(
        Guid Id,
        Guid IdProgram,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        string State
    ) : IRequest<ErrorOr<Unit>>;
}
