
namespace Mentoria.Services.Mentoring.Domain.Primitives
{
    public record DomainEvent(Guid Id) : INotification;
}
