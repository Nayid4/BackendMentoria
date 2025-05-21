namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common
{
    public record ProgramResponse(
        Guid Id,
        CareerResponse Career,
        string Name,
        string Type,
        string Description,
        int MaximumNumberOfParticipants
    );

    public record CareerResponse(
        Guid Id,
        string Name
    );
}
