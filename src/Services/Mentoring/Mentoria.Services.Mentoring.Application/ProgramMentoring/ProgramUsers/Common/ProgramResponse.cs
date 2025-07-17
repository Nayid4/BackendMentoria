namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.Common
{
    public record ProgramResponse(
        Guid Id,
        CareerResponse Career,
        string Name,
        string Type,
        string Description,
        int MaximumNumberOfParticipants
    );

}
