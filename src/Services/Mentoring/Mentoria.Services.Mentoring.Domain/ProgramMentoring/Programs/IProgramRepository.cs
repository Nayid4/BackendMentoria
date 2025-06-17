using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs
{
    public interface IProgramRepository : IGenericRepository<IdProgram, Program>
    {
        Task<Program?> GetByIdProgram(IdProgram id);
    }
}
