namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Update
{
    public record ProgramUpdateCommand(
        Guid Id,
        Guid IdCareer,
        string Name,
        string Type,
        string Description,
        int MaximumNumberOfParticipants
    ) : IRequest<ErrorOr<Unit>>;
}
