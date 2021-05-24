using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IntelligenceBattle.WebApi.Security.Configs
{
    public class AuthOptions : AuthenticationSchemeOptions
    {
        public AuthOptions()
        {

        }
        public string Issuer { get; set; } // издатель токена
        public string Audience { get; set; } // потребитель токена
        public string Key{ get; set; }   // ключ для шифрации
        public int Lifetime { get; set;} // время жизни токена - 1 минута
        public SymmetricSecurityKey SecurityKey { get; set; }
    }
}
