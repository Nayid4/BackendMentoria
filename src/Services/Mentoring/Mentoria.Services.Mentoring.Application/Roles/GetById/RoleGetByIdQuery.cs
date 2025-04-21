
using Mentoria.Services.Mentoring.Application.Roles.Common;

namespace Mentoria.Services.Mentoring.Application.Roles.GetById
{
    public record RoleGetByIdQuery(Guid Id) : IRequest<ErrorOr<RoleResponse>>;
}
