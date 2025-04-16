
namespace Mentoria.Services.Mentoring.Application.Users.Delete
{
    public record UserDeleteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
