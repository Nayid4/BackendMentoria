

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.AutomaticAssingment
{
    public record MentorAssignmentAutomaticCommand() : IRequest<ErrorOr<Unit>>;
}
