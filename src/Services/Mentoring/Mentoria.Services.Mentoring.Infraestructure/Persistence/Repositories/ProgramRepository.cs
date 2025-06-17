
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using System.Security.Cryptography;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public sealed class ProgramRepository : GenericRepository<IdProgram, Program>, IProgramRepository
    {
        public ProgramRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }

        public async Task<Program?> GetByIdProgram(IdProgram id) 
            => await _dbSet
            .Include(p => p.Career)
            .Include(p => p.Activities)
            .Include(p => p.Users)
                .ThenInclude(u => u.User)
                .ThenInclude(u => u.PersonalInformation)
            .Include(p => p.Users)
                .ThenInclude(u => u.User)
                .ThenInclude(u => u.AcademicInformation)
            .Include(p => p.Users)
                .ThenInclude(u => u.User)
                .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
