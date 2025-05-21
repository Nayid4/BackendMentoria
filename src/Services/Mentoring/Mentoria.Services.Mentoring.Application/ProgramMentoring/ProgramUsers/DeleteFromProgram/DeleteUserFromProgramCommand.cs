
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.DeleteFromProgram
{
    public record DeleteUserFromProgramCommand(
        Guid IdProgram,
        Guid IdUser
    ) : IRequest<ErrorOr<Unit>>;
}
