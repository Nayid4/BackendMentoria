
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetById
{
    public record ProgramActivityGetByIdQuery(
        Guid IdProgram,
        Guid IdProgramActivity
    ) : IRequest<ErrorOr<ProgramActivityResponse>>;
}
