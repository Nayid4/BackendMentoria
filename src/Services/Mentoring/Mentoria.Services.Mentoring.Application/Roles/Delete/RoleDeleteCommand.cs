
namespace Mentoria.Services.Mentoring.Application.Roles.Delete
{
    public record RoleDeleteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
