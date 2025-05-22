
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetAll
{
    public record ProgramActivityGetAllQuery(Guid IdProgram) : IRequest<ErrorOr<IReadOnlyList<ProgramActivityResponse>>>;
}
