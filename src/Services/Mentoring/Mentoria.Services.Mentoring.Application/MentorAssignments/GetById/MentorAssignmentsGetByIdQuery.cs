

using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetById
{
    public record MentorAssignmentsGetByIdQuery(Guid Id) : IRequest<ErrorOr<MentorAssignmentResponse>>;
}
