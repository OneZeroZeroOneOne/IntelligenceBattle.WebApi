using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IntelligenceBattle.WebApi.Security.Configs
{
    public class AuthOptions
    {
        public static string ISSUER = Environment.GetEnvironmentVariable("ISSUER"); // издатель токена
        public static string AUDIENCE = Environment.GetEnvironmentVariable("AUDIENCE"); // потребитель токена
        public static string KEY = Environment.GetEnvironmentVariable("KEY");   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
