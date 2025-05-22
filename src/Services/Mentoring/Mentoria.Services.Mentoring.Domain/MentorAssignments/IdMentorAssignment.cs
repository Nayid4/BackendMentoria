
using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.MentorAssignments
{
    public record IdMentorAssignment(Guid Value) : IIdGeneric;
}
