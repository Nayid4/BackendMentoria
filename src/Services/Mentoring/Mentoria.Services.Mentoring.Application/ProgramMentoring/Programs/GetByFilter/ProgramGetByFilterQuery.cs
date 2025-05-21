using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetByFilter
{
    public record ProgramGetByFilterQuery(
        string? SearchTerm,
        string? OrderColumn,
        string? OrderList,
        int Page,
        int SizePage
    ) : IRequest<ErrorOr<DataList<ProgramResponse>>>;
}
