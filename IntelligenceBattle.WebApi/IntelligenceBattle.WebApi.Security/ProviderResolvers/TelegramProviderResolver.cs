using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;

namespace IntelligenceBattle.WebApi.Security.ProviderResolvers
{
    class TelegramProviderResolver : IProviderResolver
    {
        private PublicContext _context;
        public TelegramProviderResolver(string token, PublicContext publicContext)
        {
            _context = publicContext;
        }

        public List<Claim> GetClaims()
        {

        }
    }
}
