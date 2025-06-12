
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.Users.ChangeState
{
    public sealed class UserChangeStateCommandHandler : IRequestHandler<UserChangeStateCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserChangeStateCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<ErrorOr<Unit>> Handle(UserChangeStateCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(command.Id)) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            if(!command.State.Equals("Aceptado") && !command.State.Equals("Pendiente"))
            {
                return Error.Conflict("User.Conflict", "El estado no es valido, ingrese Pendiente o Aceptado.");
            }

            user.ChangeState(command.State);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
