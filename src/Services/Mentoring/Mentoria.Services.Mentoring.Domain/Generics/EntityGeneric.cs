using Mentoria.Services.Mentoring.Domain.Primitives;

namespace Mentoria.Services.Mentoring.Domain.Generics
{
    public abstract class EntityGeneric<TID> : AggregateRoot
        where TID : IIdGeneric
    {
        
        public TID Id { get; protected set; } = default!;
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        public EntityGeneric()
        {
        }

        public EntityGeneric(TID id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            CreatedAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        
    }
}
