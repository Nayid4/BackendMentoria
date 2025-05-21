namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Delete
{
    public record ProgramDeleteCommand(Guid Id) :IRequest<ErrorOr<Unit>>;
}
