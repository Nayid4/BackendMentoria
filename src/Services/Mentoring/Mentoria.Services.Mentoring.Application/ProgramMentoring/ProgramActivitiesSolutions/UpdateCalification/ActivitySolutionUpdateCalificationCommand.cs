
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.UpdateCalification
{
    public record ActivitySolutionUpdateCalificationCommand(Guid IdProgramActivity, Guid IdProgramActivitySolution, decimal Calification) : IRequest<ErrorOr<Unit>>;
}
