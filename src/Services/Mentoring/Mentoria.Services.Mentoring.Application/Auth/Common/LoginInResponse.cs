

namespace Mentoria.Services.Mentoring.Application.Auth.Common
{
    public record LoginInResponse(
        string Token,
        string RefreshToken
    );
}
