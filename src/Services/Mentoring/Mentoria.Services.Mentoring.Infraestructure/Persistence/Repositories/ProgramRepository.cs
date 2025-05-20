
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class ProgramRepository : GenericRepository<IdProgram, Program>, IProgramRepository
    {
        public ProgramRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
    }
}
