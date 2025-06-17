using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers
{
    public sealed class ProgramUser : EntityGeneric<IdProgramUser>
    {
        public IdUser IdUser { get; private set; } = default!;
        public IdProgram IdProgram { get; private set; } = default!;

        public User? User { get; private set; } = default!;
        //public Program? Program { get; private set; } = default!;

        public ProgramUser() { }
        public ProgramUser(IdProgramUser id, IdUser idUser, IdProgram idProgram) : base(id)
        {
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdProgram = idProgram ?? throw new ArgumentNullException(nameof(idProgram));
        }
        public void Update(IdUser idUser, IdProgram idProgram)
        {
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdProgram = idProgram ?? throw new ArgumentNullException(nameof(idProgram));
            UpdateAt = DateTime.Now;
        }
    }
}
