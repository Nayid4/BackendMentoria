using Mentoria.Services.Mentoring.Application.Email;
using System.Net;
using System.Net.Mail;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Emails
{
    public class EmailService : IEmailService
    {
        private string _email;
        private string _password;
        private string _smtpServer;
        private int _smtpPort;

        public EmailService(string email, string password, string smtpServer, int smtpPort)
        {
            _email = email;
            _password = password;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
        }


        public async Task Send(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_email, _password),
                EnableSsl = true
            };

            var message = new MailMessage(_email!, to, subject, body);
            await smtpClient.SendMailAsync(message);
        }
    }
}
