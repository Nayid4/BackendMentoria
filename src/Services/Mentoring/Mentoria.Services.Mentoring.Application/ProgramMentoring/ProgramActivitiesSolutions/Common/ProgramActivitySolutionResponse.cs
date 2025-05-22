
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.Common
{
    public record ProgramActivitySolutionResponse(
        Guid Id,
        Guid IdProgramActivity,
        Guid IdUser,
        Guid IdFileSolution,
        double Calification
    );
}
