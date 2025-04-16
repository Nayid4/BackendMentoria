

using Mentoria.Services.Mentoring.Application.Roles.Common;
using Mentoria.Services.Mentoring.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.Roles.GetAll
{
    public sealed class RoleGetAllQueryHandler : IRequestHandler<RoleGetAllQuery, ErrorOr<IReadOnlyList<RoleResponse>>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleGetAllQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ErrorOr<IReadOnlyList<RoleResponse>>> Handle(RoleGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAll()
                .Select(role => new RoleResponse(
                    role.Id.Value,
                    role.Name
                )).ToListAsync();
        }
    }
}
