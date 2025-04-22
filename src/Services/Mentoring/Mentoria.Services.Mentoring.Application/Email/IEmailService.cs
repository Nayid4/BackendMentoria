
namespace Mentoria.Services.Mentoring.Application.Email
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string body);
    }
}
