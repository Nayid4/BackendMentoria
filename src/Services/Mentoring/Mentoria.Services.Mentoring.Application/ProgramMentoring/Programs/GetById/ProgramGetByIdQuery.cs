using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetById
{
    public record ProgramGetByIdQuery(Guid Id) : IRequest<ErrorOr<ProgramResponse>>;
}
