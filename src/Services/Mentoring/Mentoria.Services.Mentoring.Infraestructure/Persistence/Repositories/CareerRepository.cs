

using Mentoria.Services.Mentoring.Domain.Careers;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class CareerRepository : GenericRepository<IdCareer, Career>, ICareerRepository
    {
        public CareerRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }


        public async Task<Career?> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
