
namespace Mentoria.Services.Mentoring.Application.Roles.Create
{
    public record RoleCreateCommand(string Name) : IRequest<ErrorOr<Unit>>;
}
