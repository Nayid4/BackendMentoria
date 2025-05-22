
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.UpdateCalification
{
    public record ActivitySolutionUpdateCalificationCommand(Guid IdProgramActivity, Guid IdProgramActivitySolution, double Calification) : IRequest<ErrorOr<Unit>>;
}
