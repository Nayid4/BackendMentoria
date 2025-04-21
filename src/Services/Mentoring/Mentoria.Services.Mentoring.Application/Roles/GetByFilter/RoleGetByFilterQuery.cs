using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Roles.Common;

namespace Mentoria.Services.Mentoring.Application.Roles.GetByFilter
{
    public record RoleGetByFilterQuery(
        string? SearchTerm,
        string? OrderColumn,
        string? OrderList,
        int Page,
        int SizePage
    ) : IRequest<ErrorOr<DataList<RoleResponse>>>;
}
