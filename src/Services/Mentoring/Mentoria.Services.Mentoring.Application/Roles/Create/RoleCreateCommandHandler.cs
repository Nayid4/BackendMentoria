


using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Roles;

namespace Mentoria.Services.Mentoring.Application.Roles.Create
{
    public sealed class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;    
        private readonly IUnitOfWork _unitOfWork;

        public RoleCreateCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(RoleCreateCommand command, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetByNameAsync(command.Name) is Role role)
            {
                return Error.Conflict("Role.Found","El Rol ya esta registrado.");
            }

            var roleResult = new Role(
                new IdRole(Guid.NewGuid()),
                command.Name
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
