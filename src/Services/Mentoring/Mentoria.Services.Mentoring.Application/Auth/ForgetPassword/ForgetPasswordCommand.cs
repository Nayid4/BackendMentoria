

namespace Mentoria.Services.Mentoring.Application.Auth.ForgetPassword
{
    public record ForgetPasswordCommand(Guid Id, string Password, string PasswordVerify) : IRequest<ErrorOr<Unit>>;
}
