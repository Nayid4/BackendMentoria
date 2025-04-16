
using Mentoria.Services.Mentoring.Domain.AcademicInformations;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class AcademicInformationRepository : GenericRepository<IdAcademicInformation, AcademicInformation>, IAcademicInformationRepository
    {
        public AcademicInformationRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }

        public async Task<AcademicInformation?> GetByCode(string code)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<AcademicInformation?> GetByEmail(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
