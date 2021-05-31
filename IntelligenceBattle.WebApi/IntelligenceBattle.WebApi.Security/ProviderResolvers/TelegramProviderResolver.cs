using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using System;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.ProviderResolvers
{
    class TelegramProviderResolver : IProviderResolver
    {
        public PublicContext context;
        public string token;
        public TelegramProviderResolver(string t, PublicContext publicContext)
        {
            context = publicContext;
            token = t;
        }

        public async Task<List<Claim>> GetClaims()
        {
            var tokenParameters = token.Split(";");
            var parameters = new Dictionary<string, string>();
            foreach (var v in tokenParameters)
            {
                parameters[v.Split("=").First()] = v.Split("=").Last();
            }
            var key = tokenParameters.First();
            var provider = await context.AuthorizationProviders.Include(x => x.AuthorizationProviderType).ThenInclude((x => x.AuthorizationProviderCenter)).FirstOrDefaultAsync(x => x.Key == key);
            if (provider == null)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ProviderKeyNotFound, "provider key not found");
            }

            if (provider.AuthorizationProviderType.AuthorizationProviderCenter.Title != "Telegram")
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.AuthCenterNotFound,
                    "provider key not for telegram provider resolver");
            }

            if (parameters.ContainsKey("RealId"))
            {
                var userSec = await context.UserSecurities.Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.RealId == int.Parse(parameters["RealId"]));
                if (userSec != null)
                {
                    return new List<Claim>
                    {
                        new Claim("UserId", Convert.ToString(userSec.User.Id)),
                        new Claim("AuthCenterId", Convert.ToString(userSec.AuthorizationCenterId)),
                        new Claim("ProviderId", Convert.ToString(provider.Id)),
                    };
                }
                else
                {
                    throw ExceptionFactory.SoftException(ExceptionEnum.UserNotFound, "UserNotFound");
                }
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.RealIdIsAbsent, "RealIdIsAbsent");
            }
        }
    }
}
