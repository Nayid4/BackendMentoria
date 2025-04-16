
using Mentoria.Services.Mentoring.Application.Users.Common;

namespace Mentoria.Services.Mentoring.Application.Users.GetAll
{
    public record UserGetAllQuery() : IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;
}
