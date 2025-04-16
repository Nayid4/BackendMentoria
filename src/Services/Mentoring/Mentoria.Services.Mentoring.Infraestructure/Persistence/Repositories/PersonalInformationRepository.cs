

using Mentoria.Services.Mentoring.Domain.PersonalInformations;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class PersonalInformationRepository : GenericRepository<IdPersonalInformation, PersonalInformation>, IPersonalInformationRepository
    {
        public PersonalInformationRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
    }
}
