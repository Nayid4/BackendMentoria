
using Mentoria.Services.Mentoring.Application.Users.Common;

namespace Mentoria.Services.Mentoring.Application.Users.GetById
{
    public record UserGetByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponse>>;
}
