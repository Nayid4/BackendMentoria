

using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Roles
{
    public sealed class Role : EntityGeneric<IdRole>
    {
        public string Name { get; private set; } = string.Empty;
        public Role()
        {
        }

        public Role(IdRole id, string name) : base(id)
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
