

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Roles
{
    public interface IRoleRepository : IGenericRepository<IdRole, Role>
    {
        Task<Role?> GetByNameAsync(string name);
    }
}
