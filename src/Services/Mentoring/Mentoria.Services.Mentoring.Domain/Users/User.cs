using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Roles;

namespace Mentoria.Services.Mentoring.Domain.Users
{
    public sealed class User : EntityGeneric<IdUser>
    {

        public IdPersonalInformation IdPersonalInformation { get; private set; } = default!;
        public IdRole IdRole { get; private set; } = default!;
        public IdAcademicInformation IdAcademicInformation { get; private set; } = default!;
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        public PersonalInformation? PersonalInformation { get; private set; }
        public AcademicInformation? AcademicInformation { get; private set; }
        public Role? Role { get; private set; } = default!;

        public User() { }

        public User(IdUser id, IdPersonalInformation idPersonalInformation, IdRole idRole, IdAcademicInformation idAcademicInformation, string userName, string password, string state) 
            : base(id)
        {
            IdPersonalInformation = idPersonalInformation ?? throw new ArgumentNullException(nameof(idPersonalInformation));
            IdAcademicInformation = idAcademicInformation ?? throw new ArgumentNullException(nameof(idAcademicInformation));
            IdRole = idRole ?? throw new ArgumentNullException(nameof(idRole));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void Update(IdPersonalInformation idPersonalInformation, IdRole idRole, IdAcademicInformation idAcademicInformation, string userName, string state) 
        {
            IdPersonalInformation = idPersonalInformation ?? throw new ArgumentNullException(nameof(idPersonalInformation));
            IdAcademicInformation = idAcademicInformation ?? throw new ArgumentNullException(nameof(idAcademicInformation));
            IdRole = idRole ?? throw new ArgumentNullException(nameof(idRole));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            UpdateAt = DateTime.Now;
        }

        public void UpdatePassword(string password)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
            UpdateAt = DateTime.Now;
        }

        public void ChangeState(string state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
            UpdateAt = DateTime.Now;
        }

    }
}
