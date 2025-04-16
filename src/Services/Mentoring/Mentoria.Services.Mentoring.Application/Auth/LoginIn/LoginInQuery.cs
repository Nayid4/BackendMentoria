

using Mentoria.Services.Mentoring.Application.Auth.Common;

namespace Mentoria.Services.Mentoring.Application.Auth.LoginIn
{
    public record LoginInQuery(string UserName, string Password) : IRequest<ErrorOr<LoginInResponse>>;
}
