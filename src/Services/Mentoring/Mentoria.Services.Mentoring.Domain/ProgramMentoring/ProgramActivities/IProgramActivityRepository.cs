
using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities
{
    public interface IProgramActivityRepository : IGenericRepository<IdProgramActivity, ProgramActivity>
    {
        Task<ProgramActivity?> GetByIdProgramActivity(IdProgramActivity id);
    }
}
