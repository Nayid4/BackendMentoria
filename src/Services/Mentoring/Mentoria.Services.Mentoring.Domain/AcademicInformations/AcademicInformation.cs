

using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.AcademicInformations
{
    public sealed class AcademicInformation : EntityGeneric<IdAcademicInformation>
    {
        public string Code { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public IdCareer IdCareer { get; private set; } = default!;
        public int Cicle { get; private set; }
        public string Expectative { get; private set; } = string.Empty;

        public Career? Career { get; private set; } = default!;

        public AcademicInformation(IdAcademicInformation id, string code, string email, IdCareer idCareer, int cicle, string expectative)
            : base(id)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            IdCareer = idCareer ?? throw new ArgumentNullException(nameof(idCareer));
            Cicle = cicle;
            Expectative = expectative ?? throw new ArgumentNullException(nameof(expectative));
        }

        public void Update(string code, string email, IdCareer idCareer, int cicle, string expectative)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            IdCareer = idCareer ?? throw new ArgumentNullException(nameof(idCareer));
            Cicle = cicle;
            Expectative = expectative ?? throw new ArgumentNullException(nameof(expectative));
            UpdateAt = DateTime.Now;
        }
    }
}
