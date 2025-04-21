using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Careers.Common;

namespace Mentoria.Services.Mentoring.Application.Careers.GetByFilter
{
    public record CareerGetByFilterQuery(
        string? SearchTerm,
        string? OrderColumn,
        string? OrderList,
        int Page,
        int SizePage
    ) : IRequest<ErrorOr<DataList<CareerResponse>>>;
}
