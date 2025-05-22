
using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Users.Common;

namespace Mentoria.Services.Mentoring.Application.Users.GetByFilter
{
    public record UserGetByFilterQuery(
        string? SearchTerm,
        Guid? RoleId,
        string? State,
        string? OrderColumn,
        string? OrderList,
        int Page,
        int SizePage
    ) : IRequest<ErrorOr<DataList<UserResponse>>>;

}
