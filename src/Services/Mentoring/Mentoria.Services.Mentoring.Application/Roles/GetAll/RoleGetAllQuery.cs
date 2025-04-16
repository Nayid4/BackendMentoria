using Mentoria.Services.Mentoring.Application.Roles.Common;

namespace Mentoria.Services.Mentoring.Application.Roles.GetAll
{
    public record RoleGetAllQuery() : IRequest<ErrorOr<IReadOnlyList<RoleResponse>>>;
}
