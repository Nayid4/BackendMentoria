
namespace Mentoria.Services.Mentoring.Application.Users.ChangeState
{
    public record UserChangeStateCommand(
        Guid Id,
        string State
    ) : IRequest<ErrorOr<Unit>>;
}
