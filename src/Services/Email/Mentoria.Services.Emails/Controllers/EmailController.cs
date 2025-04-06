using Mentoria.Services.Emails.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Mentoria.Services.Emails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController
    {
        [HttpPost(Name = "Send")]
        public Task<bool> Send(EmailDto emailDto)
        {
            return Task.FromResult(true);
        }
    }
}
