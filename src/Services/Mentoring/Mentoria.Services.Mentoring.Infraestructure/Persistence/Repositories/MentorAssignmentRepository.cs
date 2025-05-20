
using Mentoria.Services.Mentoring.Domain.MentorAssignments;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class MentorAssignmentRepository : GenericRepository<IdMentorAssignment, MentorAssignment>, IMentorAssignmentRepository
    {
        public MentorAssignmentRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
    }
}
