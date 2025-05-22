
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetByIdUser
{
    public record ActivitySolutionGetByIdUserQuery(
        Guid IdProgramActivity,
        Guid IdUser
    ) : IRequest<ErrorOr<ProgramActivitySolutionResponse>>;
}
