using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Mentoria.Services.Mentoring.Domain.DTOs;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mentoria.Services.Mentoring.Application.Utils
{
    public class AuthToken : IAuthToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthToken(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GeneratePass(int longitud = 12)
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";

            var bytes = new byte[longitud];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            var resultado = new StringBuilder(longitud);
            foreach (var b in bytes)
            {
                // Elegimos un carácter al azar de la cadena
                resultado.Append(caracteres[b % caracteres.Length]);
            }

            return resultado.ToString();
        }

        public string EncryptSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el Hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        public string GenerateJWT(User user, int option)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            // Crear la informacion del usuario para token
            var UserClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
                new Claim(ClaimTypes.Name, user.PersonalInformation!.Name + ' ' + user.PersonalInformation.LastName),
                new Claim(ClaimTypes.Role, user.Role!.Name.ToString()),
                new Claim(ClaimTypes.GivenName, user.UserName),
            };

            // Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                _configuration["Jwt:issuer"],
                _configuration["Jwt:Audience"],
                claims: UserClaims,
                expires: option switch
                {
                    1 => DateTime.Now.AddMinutes(60),
                    2 => DateTime.Now.AddMinutes(80),
                    _ => throw new ArgumentOutOfRangeException(nameof(option), "Opción no válida.")
                },
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        public UserDataDTO? DecodeJWT()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return null;
            }

            var userClaims = identity.Claims;

            var idClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var nameClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
            var roleClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;
            var userNameClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.GivenName)?.Value;

            if (
                Guid.TryParse(idClaim, out var id) &&
                !string.IsNullOrEmpty(nameClaim) &&
                !string.IsNullOrEmpty(roleClaim) &&
                !string.IsNullOrEmpty(userNameClaim)
            )
            {
                return new UserDataDTO(
                    id,
                    nameClaim,
                    roleClaim,
                    userNameClaim
                );
            }

            return null;
        }
    }
}
