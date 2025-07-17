

using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetAll
{
    public record MentorAssignmentsGetAllQuery() : IRequest<ErrorOr<IReadOnlyList<MentorAssignmentResponse>>>;
}
