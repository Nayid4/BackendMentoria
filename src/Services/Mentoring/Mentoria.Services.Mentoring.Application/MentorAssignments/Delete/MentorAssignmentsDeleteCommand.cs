
namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Delete
{
    public record MentorAssignmentsDeleteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
