

using Mentoria.Services.Mentoring.Application.Auth.Common;
using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.DTOs;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.Auth.RefreshToken
{
    public sealed class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ErrorOr<LoginInResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthToken _authToken;

        public RefreshTokenQueryHandler(IUserRepository userRepository, IAuthToken authToken)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
        }
        public async Task<ErrorOr<LoginInResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (_authToken.DecodeJWT() is not UserDataDTO datosUsuario)
            {
                return Error.NotFound("Autenticacion.TokenNoEcontrado", "No se ha encontrado el token del usuario.");
            }

            if (await _userRepository.GetByIdUser(new IdUser(datosUsuario.Id)) is not User usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            var respuesta = new LoginInResponse(
                _authToken.GenerateJWT(usuario, 1),
                _authToken.GenerateJWT(usuario, 2)
            );

            return respuesta;
        }
    }
}
