
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.AddToProgram
{
    public record AddProgramActivityToProgramCommand(
        Guid IdProgram,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        string State
    ) : IRequest<ErrorOr<Unit>>;
}
