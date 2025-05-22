
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.AddToProgram
{
    public record AddUserToProgramCommand(Guid IdProgram, Guid IdUser) : IRequest<ErrorOr<Unit>>;
}
