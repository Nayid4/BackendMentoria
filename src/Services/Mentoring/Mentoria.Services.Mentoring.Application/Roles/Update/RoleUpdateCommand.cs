

namespace Mentoria.Services.Mentoring.Application.Roles.Update
{
    public record RoleUpdateCommand(
        Guid Id,
        string Name
    ) : IRequest<ErrorOr<Unit>>;
}
