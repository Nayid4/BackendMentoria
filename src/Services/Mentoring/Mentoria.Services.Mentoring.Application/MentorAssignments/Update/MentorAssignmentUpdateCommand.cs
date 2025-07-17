

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Update
{
    public record MentorAssignmentUpdateCommand(Guid Id, Guid IdMentor, Guid IdUser) : IRequest<ErrorOr<Unit>>;
}
