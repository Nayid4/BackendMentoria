

using Mentoria.Services.Mentoring.Domain.DTOs;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.Utils
{
    public interface IAuthToken
    {
        string GeneratePass(int longitud = 12);
        string EncryptSHA256(string text);
        string GenerateJWT(User user, int option);
        UserDataDTO? DecodeJWT();
    }
}
