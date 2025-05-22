
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.GetAll
{
    public record ProgramUserGetAllQuery(Guid IdProgram) : IRequest<ErrorOr<IReadOnlyList<ProgramUserResponse>>>;
}
