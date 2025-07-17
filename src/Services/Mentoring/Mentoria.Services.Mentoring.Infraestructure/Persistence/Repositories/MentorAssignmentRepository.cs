
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class MentorAssignmentRepository : GenericRepository<IdMentorAssignment, MentorAssignment>, IMentorAssignmentRepository
    {
        public MentorAssignmentRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }

        public IQueryable<MentorAssignment> GetAllMentorAssignment()
        {
            return _dbSet
                .Include(x => x.User)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.Role)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.Role)
                .AsNoTracking();
        }

        public IQueryable<MentorAssignment> GetByMentorId(IdUser mentorId)
        {
            return _dbSet
                .Include(x => x.User)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.Role)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.Role)
                .Select(x => x)
                .Where(x => x.IdMentor == mentorId)
                .AsNoTracking();
        }

        public IQueryable<MentorAssignment> GetByUserId(IdUser userId)
        {
            return _dbSet
                .Include(x => x.User)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.User)
                    .ThenInclude(u => u.Role)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.PersonalInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.AcademicInformation)
                .Include(x => x.Mentor)
                    .ThenInclude(u => u.Role)
                .Select(x => x)
                .Where(x => x.IdUser == userId)
                .AsNoTracking();
        }
    }
}
