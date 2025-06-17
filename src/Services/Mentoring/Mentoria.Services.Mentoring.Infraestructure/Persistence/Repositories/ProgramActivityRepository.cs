
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class ProgramActivityRepository : GenericRepository<IdProgramActivity, ProgramActivity>, IProgramActivityRepository
    {
        public ProgramActivityRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }

        public async Task<ProgramActivity?> GetByIdProgramActivity(IdProgramActivity id)
        {
            return await _dbSet
                .Include(p => p.ProgramActivitySolutions)
                    .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
