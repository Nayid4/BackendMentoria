
namespace Mentoria.Services.Mentoring.Application.Users.ResetPassword
{
    public record ResetPasswordUserCommand(Guid Id, string Password, string PasswordVerify) : IRequest<ErrorOr<Unit>>;
}
