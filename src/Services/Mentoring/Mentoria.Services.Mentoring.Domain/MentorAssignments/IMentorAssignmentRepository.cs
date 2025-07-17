
using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.MentorAssignments
{
    public interface IMentorAssignmentRepository : IGenericRepository<IdMentorAssignment, MentorAssignment>
    {
        IQueryable<MentorAssignment> GetByMentorId(IdUser mentorId);
        IQueryable<MentorAssignment> GetByUserId(IdUser userId);
        IQueryable<MentorAssignment> GetAllMentorAssignment();
    }
}
