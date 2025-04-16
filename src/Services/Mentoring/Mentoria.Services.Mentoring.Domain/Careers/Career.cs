

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Careers
{
    public sealed class Career : EntityGeneric<IdCareer>
    {
        public string Name { get; private set; } = string.Empty;

        public Career()
        {
        }

        public Career(IdCareer id, string name) : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Update(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UpdateAt = DateTime.Now;
        }
    }
}
