
using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Users
{
    public interface IUserRepository : IGenericRepository<IdUser, User>
    {
        Task<User?> LoginIn(string userName, string password);
        Task<User?> GetByUserName(string userName);
        Task<User?> GetByIdUser(IdUser id);
        IQueryable<User> GetAllUsers();
    }
}
