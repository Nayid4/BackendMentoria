

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.PersonalInformations
{
    public class PersonalInformation : EntityGeneric<IdPersonalInformation>
    {
        public string DNI { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Sex { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;

        public PersonalInformation(IdPersonalInformation id, string dNI, string name, string lastName, string sex, string phone)
            : base(id)
        {
            DNI = dNI ?? throw new ArgumentNullException(nameof(dNI));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }

        public void Update(string dNI, string name, string lastName, string sex, string phone)
        {
            DNI = dNI ?? throw new ArgumentNullException(nameof(dNI));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            UpdateAt = DateTime.Now;
        }
    }
}
