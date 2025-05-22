
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class ProgramActivityRepository : GenericRepository<IdProgramActivity, ProgramActivity>, IProgramActivityRepository
    {
        public ProgramActivityRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
    }
}
