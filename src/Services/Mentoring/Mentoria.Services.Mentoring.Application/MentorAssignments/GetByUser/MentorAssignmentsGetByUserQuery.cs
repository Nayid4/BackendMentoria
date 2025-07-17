
using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetByUser
{
    public record MentorAssignmentsGetByUserQuery(Guid IdUser) : IRequest<ErrorOr<MentorAssignmentUsersResponse>>;
}
