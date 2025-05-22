using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions
{
    public sealed class ProgramActivitySolution : EntityGeneric<IdProgramActivitySolution>
    {
        public IdProgramActivity IdProgramActivity { get; private set; } = default!;
        public IdUser IdUser { get; private set; } = default!;
        public Guid IdFileSolution { get; private set; } = default!;
        public double Calification { get; private set; }

        public ProgramActivity? ProgramActivity { get; private set; } = default!;
        public User? User { get; private set; } = default!;

        public ProgramActivitySolution() { }

        public ProgramActivitySolution(IdProgramActivitySolution id, IdProgramActivity idProgramActivity, IdUser idUser, Guid idFileSolution, double calification)
            : base(id)
        {
            IdProgramActivity = idProgramActivity ?? throw new ArgumentNullException(nameof(idProgramActivity));
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdFileSolution = idFileSolution;
            Calification = calification;
        }

        public void Update(IdProgramActivity idProgramActivity, IdUser idUser, Guid idFileSolution, double calification)
        {
            IdProgramActivity = idProgramActivity ?? throw new ArgumentNullException(nameof(idProgramActivity));
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdFileSolution = idFileSolution;
            Calification = calification;
            UpdateAt = DateTime.Now;
        }

        public void UpdateCalification(double calification)
        {
            Calification = calification;
            UpdateAt = DateTime.Now;
        }
    }
}
