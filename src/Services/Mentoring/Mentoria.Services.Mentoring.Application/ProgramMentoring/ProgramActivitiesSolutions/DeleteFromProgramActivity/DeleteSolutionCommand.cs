
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.DeleteFromProgramActivity
{
    public record DeleteSolutionCommand(Guid IdProgramActivity, Guid IdProgramActivitySolution) : IRequest<ErrorOr<Unit>>;
}
