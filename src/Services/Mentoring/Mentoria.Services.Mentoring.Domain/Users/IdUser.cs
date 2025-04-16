using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Domain.Users
{
    public record IdUser(Guid Value) : IIdGeneric;
}
