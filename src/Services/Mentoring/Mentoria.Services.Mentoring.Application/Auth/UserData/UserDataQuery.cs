
using Mentoria.Services.Mentoring.Domain.DTOs;

namespace Mentoria.Services.Mentoring.Application.Auth.UserData
{
    public record UserDataQuery() : IRequest<ErrorOr<UserDataDTO>>;
}
