using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace IntelligenceBattle.WebApi.Security.Services
{
    public class JwtTokenService
    {
        public ClaimsIdentity GetIdentity(string username, int userId)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimsIdentity.DefaultNameClaimType, ),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, )
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
