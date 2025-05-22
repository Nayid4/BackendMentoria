using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetAll
{
    public record ProgramGetAllQuery() : IRequest<ErrorOr<IReadOnlyList<ProgramResponse>>>;
}
