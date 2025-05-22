
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.AddToProgramActivity
{
    public record AddSolutionToProgramActivityCommand(
        Guid IdProgramActivity,
        Guid IdUser,
        Guid IdFileSolution
    ) : IRequest<ErrorOr<Unit>>;
}
