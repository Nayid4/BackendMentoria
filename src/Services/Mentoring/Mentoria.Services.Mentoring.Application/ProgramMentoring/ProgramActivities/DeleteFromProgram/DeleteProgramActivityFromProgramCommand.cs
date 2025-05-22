
namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.DeleteFromProgram
{
    public record DeleteProgramActivityFromProgramCommand(
        Guid IdProgram,
        Guid IdProgramActivity
    ) : IRequest<ErrorOr<Unit>>;
}
