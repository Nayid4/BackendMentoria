
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Roles;

namespace Mentoria.Services.Mentoring.Application.Roles.Delete
{
    public sealed class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleDeleteCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(RoleDeleteCommand command, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetById(new IdRole(command.Id)) is not Role role)
            {
                return Error.NotFound("Rol.NoEncontrado", "El rol no existe.");
            }

            _roleRepository.Delete(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
