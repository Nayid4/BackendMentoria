
namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Create
{
    public record MentorAssignmentCreateCommand(Guid IdMentor, Guid IdUser) : IRequest<ErrorOr<Unit>>;

}
