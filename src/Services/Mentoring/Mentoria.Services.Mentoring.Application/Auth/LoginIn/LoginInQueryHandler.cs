
using Mentoria.Services.Mentoring.Application.Auth.Common;
using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.Users;


namespace Mentoria.Services.Mentoring.Application.Auth.LoginIn
{
    public sealed class LoginInQueryHandler : IRequestHandler<LoginInQuery, ErrorOr<LoginInResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthToken _authToken;

        public LoginInQueryHandler(IUserRepository userRepository, IAuthToken authToken)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
        }

        public async Task<ErrorOr<LoginInResponse>> Handle(LoginInQuery query, CancellationToken cancellationToken)
        {
            string pass = _authToken.EncryptSHA256(query.Password);

            if (await _userRepository.LoginIn(query.UserName, pass) is not User user)
            {
                return Error.NotFound("Autenticacion.NoEncontrado", "Usuario o Contraseña incorrectos.");
            }

            var respuesta = new LoginInResponse(
                _authToken.GenerateJWT(user, 1),
                _authToken.GenerateJWT(user, 2)

            );

            return respuesta;
        }
    }
}
