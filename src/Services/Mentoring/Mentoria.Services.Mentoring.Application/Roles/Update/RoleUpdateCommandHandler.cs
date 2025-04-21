using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Roles;

namespace Mentoria.Services.Mentoring.Application.Roles.Update
{
    public sealed class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommand, ErrorOr<Unit>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleUpdateCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetById(new IdRole(request.Id)) is not Role role)
            {
                return Error.NotFound("Rol.NoEncontrado", "El rol no existe.");
            }

            if(await _roleRepository.GetByNameAsync(request.Name) is Role roleExist && !role.Name.Equals(roleExist.Name))
            {
                return Error.Conflict("Rol.Registrado", "El rol ya esta registrado.");
            }

            role.Update(
                request.Name
            );

            _roleRepository.Update(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
