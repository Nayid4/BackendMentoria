

using Mentoria.Services.Mentoring.Domain.Roles;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<IdRole, Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
