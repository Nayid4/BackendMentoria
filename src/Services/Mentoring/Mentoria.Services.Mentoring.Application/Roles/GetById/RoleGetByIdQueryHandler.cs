
using Mentoria.Services.Mentoring.Application.Roles.Common;
using Mentoria.Services.Mentoring.Domain.Roles;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mentoria.Services.Mentoring.Application.Roles.GetById
{
    public sealed class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, ErrorOr<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleGetByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ErrorOr<RoleResponse>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetById(new IdRole(request.Id)) is not Role role)
            {
                return Error.NotFound("Rol.NoEncontrado", "El rol no existe.");
            }

            var response = new RoleResponse(
                role.Id.Value,
                role.Name
            );

            return response;
        }
    }
}
