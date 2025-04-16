using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.DTOs;


namespace Mentoria.Services.Mentoring.Application.Auth.UserData
{
    public sealed class UserDataQueryHandler : IRequestHandler<UserDataQuery, ErrorOr<UserDataDTO>>
    {
        private readonly IAuthToken _authToken;

        public UserDataQueryHandler(IAuthToken authToken)
        {
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
        }

        public Task<ErrorOr<UserDataDTO>> Handle(UserDataQuery request, CancellationToken cancellationToken)
        {
            if (_authToken.DecodeJWT() is not UserDataDTO userData)
            {
                return Task.FromResult<ErrorOr<UserDataDTO>>(
                    Error.NotFound("Autenticacion.TokenNoEcontrado", "No se ha encontrado el token del usuario.")
                );
            }

            return Task.FromResult<ErrorOr<UserDataDTO>>(userData);
        }

    }
}
