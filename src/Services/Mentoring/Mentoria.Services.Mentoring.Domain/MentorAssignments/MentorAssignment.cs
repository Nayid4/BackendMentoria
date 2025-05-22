
using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.MentorAssignments
{
    public sealed class MentorAssignment : EntityGeneric<IdMentorAssignment>
    {
        public IdUser IdUser { get; private set; } = default!;
        public IdUser IdMentor { get; private set; } = default!;

        public User? User { get; private set; } = default!;
        public User? Mentor { get; private set; } = default!;

        public MentorAssignment() { }
        public MentorAssignment(IdMentorAssignment id, IdUser idUser, IdUser idMentor, string state) : base(id)
        {
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdMentor = idMentor ?? throw new ArgumentNullException(nameof(idMentor));
        }

        public void Update(IdUser idUser, IdUser idMentor)
        {
            IdUser = idUser ?? throw new ArgumentNullException(nameof(idUser));
            IdMentor = idMentor ?? throw new ArgumentNullException(nameof(idMentor));
            UpdateAt = DateTime.Now;
        }
    }
}
