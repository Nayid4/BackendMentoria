namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Create
{
    public record ProgramCreateCommand(
        Guid IdCareer,
        string Name,
        string Type,
        string Description,
        int MaximumNumberOfParticipants
    ) : IRequest<ErrorOr<Unit>>;
}
