

using Mentoria.Services.Mentoring.Application.Auth.Common;

namespace Mentoria.Services.Mentoring.Application.Auth.RefreshToken
{
    public record RefreshTokenQuery() : IRequest<ErrorOr<LoginInResponse>>;
}
