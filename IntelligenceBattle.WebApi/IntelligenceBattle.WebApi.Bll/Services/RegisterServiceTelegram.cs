using System;
using IntelligenceBattle.WebApi.Bll.Abstractions;
using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class RegisterServiceTelegram : IRegisterService
    {
        public readonly PublicContext context;
        public string token;
        public RegisterServiceTelegram(string t, PublicContext publicContext)
        {
            context = publicContext;
            token = t;
        }

        public string GetAuthCenterName()
        {

            return "telegram";

        }

        public async Task<int> GetProviderId()
        {
            var provider = await context.AuthorizationProviders.FirstOrDefaultAsync(x => x.Key == token);
            return provider.Id;
        }


        public async Task<User> RegisterUser(RegisterInModel registerInModel)
        {
            var provider = await context.AuthorizationProviders.Include(x => x.AuthorizationProviderType).ThenInclude((x => x.AuthorizationProviderCenter)).FirstOrDefaultAsync(x => x.Key == token);
            if (provider == null)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ProviderKeyNotFound, "provider key not found");
            }

            if (provider.AuthorizationProviderType.AuthorizationProviderCenter.Title != "Telegram")
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.AuthCenterNotFound,
                    "provider key not for telegram register service");
            }
            var user = new User
            {
                Name = registerInModel.Name,
                Surname = registerInModel.Surname,
                CreatedDatetime = DateTime.Now,
            };
            user.UserSecurities.Add(new UserSecurity
            {
                RealId = registerInModel.RealId,
                AuthorizationCenterId = provider.AuthorizationProviderType.AuthorizationProviderCenterId,
            });
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
