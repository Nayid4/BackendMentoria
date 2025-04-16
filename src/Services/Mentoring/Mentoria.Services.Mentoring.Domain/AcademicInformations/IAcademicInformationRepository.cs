

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.AcademicInformations
{
    public interface IAcademicInformationRepository : IGenericRepository<IdAcademicInformation, AcademicInformation>
    {
        Task<AcademicInformation?> GetByEmail(string email);
        Task<AcademicInformation?> GetByCode(string code);
    }
}
