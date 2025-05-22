
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetAll
{
    public record ActivitySolutionGetAllQuery(Guid IdProgramActivity) : IRequest<ErrorOr<IReadOnlyList<ProgramActivitySolutionResponse>>>;
}
