

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Careers
{
    public interface ICareerRepository : IGenericRepository<IdCareer, Career>
    {
        Task<Career?> GetByNameAsync(string name);
    }
}
