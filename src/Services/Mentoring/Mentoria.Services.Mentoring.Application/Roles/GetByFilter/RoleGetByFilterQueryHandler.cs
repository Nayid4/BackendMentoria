
using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Roles.Common;
using Mentoria.Services.Mentoring.Domain.Roles;
using System.Data;
using System.Linq.Expressions;

namespace Mentoria.Services.Mentoring.Application.Roles.GetByFilter
{
    public sealed class RoleGetByFilterQueryHandler : IRequestHandler<RoleGetByFilterQuery, ErrorOr<DataList<RoleResponse>>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleGetByFilterQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<ErrorOr<DataList<RoleResponse>>> Handle(RoleGetByFilterQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                roles = roles.Where(at =>
                    at.Name.ToLower().Contains(request.SearchTerm.ToLower())
                );
            }

            if (request.OrderList?.ToLower() == "desc")
            {
                roles = roles.OrderByDescending(ListarOrdenDePropiedad(request));
            }
            else
            {
                roles = roles.OrderBy(ListarOrdenDePropiedad(request));
            }



            var result = roles.Select(role => new RoleResponse(
                role.Id.Value,
                role.Name
            ));

            var ListRole = await DataList<RoleResponse>.CreateAsync(
                    result,
                    request.Page,
                    request.SizePage
                );

            return ListRole;
        }

        private static Expression<Func<Role, object>> ListarOrdenDePropiedad(RoleGetByFilterQuery request)
        {
            return request.OrderColumn?.ToLower() switch
            {
                "name" => role => role.Name,
                _ => role => role.Id
            };
        }
    }
}
