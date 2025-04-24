
using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mentoria.Services.Mentoring.Application.Auth.ForgetPassword
{
    public sealed class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthToken _authToken;
        private readonly IUnitOfWork _unitOfWork;

        public ForgetPasswordCommandHandler(IUserRepository userRepository, IAuthToken authToken, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ForgetPasswordCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(command.Id)) is not User user)
            {
                return Error.NotFound("Autenticacion.NoEncontrado", "Usuario o Contraseña incorrectos.");
            }

            if (user.Password.Equals(_authToken.EncryptSHA256(command.Password)))
            {
                return Error.Conflict("Autenticacion.Conflict", "La nueva contraseña no puede ser igual a la anterior.");
            }

            if (command.Password != command.PasswordVerify)
            {
                return Error.Conflict("Autenticacion.Conflict", "Las contraseñas no coinciden.");
            }

            user.UpdatePassword(_authToken.EncryptSHA256(command.Password));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
