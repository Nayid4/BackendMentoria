
using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetByMentor
{
    public record MentorAssignmentsGetByMentorQuery(Guid IdMentor) : IRequest<ErrorOr<MentorAssignmentUsersResponse>>;
}
