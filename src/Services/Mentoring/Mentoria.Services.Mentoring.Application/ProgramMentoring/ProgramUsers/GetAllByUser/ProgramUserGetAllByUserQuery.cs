
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.GetAllByUser
{
    public record ProgramUserGetAllByUserQuery(Guid IdUser) : IRequest<ErrorOr<IReadOnlyList<ProgramResponse>>>;
}
