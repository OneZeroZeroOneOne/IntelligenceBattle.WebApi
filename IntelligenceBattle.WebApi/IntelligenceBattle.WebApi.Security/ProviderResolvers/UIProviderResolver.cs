using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Abstractions;
using IntelligenceBattle.WebApi.Security.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.ProviderResolvers
{
    public class UIProviderResolver : IProviderResolver    
    {
        private JwtTokenService _jwtTokenService;
        public UIProviderResolver(string token, JwtTokenService jwtTokenService)
        {

        }

        public List<Claim> GetClaims()
        {
            
        }
    }
}
