using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions
{
    public sealed class ProgramActivitySolution : EntityGeneric<IdProgramActivitySolution>
    {
        public IdProgramActivity IdProgramActivity { get; private set; } = default!;
        public Guid IdFileSolution { get; private set; } = default!;
        public decimal Calification { get; private set; }

        public ProgramActivitySolution() { }

        public ProgramActivitySolution(IdProgramActivitySolution id, IdProgramActivity idProgramActivity, Guid idFileSolution, decimal calification)
            : base(id)
        {
            IdProgramActivity = idProgramActivity ?? throw new ArgumentNullException(nameof(idProgramActivity));
            IdFileSolution = idFileSolution;
            Calification = calification;
        }

        public void Update(IdProgramActivity idProgramActivity, Guid idFileSolution, decimal calification)
        {
            IdProgramActivity = idProgramActivity ?? throw new ArgumentNullException(nameof(idProgramActivity));
            IdFileSolution = idFileSolution;
            Calification = calification;
            UpdateAt = DateTime.Now;
        }

        public void UpdateCalification(decimal calification)
        {
            Calification = calification;
            UpdateAt = DateTime.Now;
        }
    }
}
