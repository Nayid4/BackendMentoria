

using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<IdUser, User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }

        public IQueryable<User> GetAllUsers()
        {
            return _dbSet
                .Include(u => u.PersonalInformation)
                .Include(u => u.AcademicInformation)
                .Include(u => u.Role)
                .AsNoTracking();
        }


        public async Task<User?> GetByIdUser(IdUser id)
        {
            return await _dbSet
                .Include(u => u.PersonalInformation)
                .Include(u => u.AcademicInformation)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<User?> LoginIn(string userName, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}
