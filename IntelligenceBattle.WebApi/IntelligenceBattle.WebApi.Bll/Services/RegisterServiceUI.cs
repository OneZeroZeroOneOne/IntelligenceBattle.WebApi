using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using System;
using IntelligenceBattle.WebApi.Bll.Abstractions;
using IntelligenceBattle.WebApi.Dal.Models.In;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class RegisterServiceUI : IRegisterService
    {
        public PublicContext context;
        public string token;
        public RegisterServiceUI(string t, PublicContext publicContext)
        {
            context = publicContext;
            token = t;
        }

        public string GetAuthCenterName()
        {
            return "bearer";
        }

        public async Task<int> GetProviderId()
        {
            return 1;
        }

        public async Task<User> RegisterUser(RegisterInModel registerInModel)
        {
            if (registerInModel.Login != null && registerInModel.Password != null)
            {
                var userSec = await context.UserSecurities
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Login == registerInModel.Login);
                if (userSec != null)
                {
                    throw ExceptionFactory.FriendlyException(ExceptionEnum.LoginExist, "login already exist");
                }
                var user = new User
                {
                    Name = registerInModel.Login,
                    CreatedDatetime = DateTime.Now,
                };
                user.UserSecurities.Add(new UserSecurity
                {
                    Login = registerInModel.Login,
                    Password = registerInModel.Password,
                    AuthorizationCenterId = 1,
                });
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return user;
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.InvalidLoginOrPassword, "InvalidLoginOrPassword");
            }
        }

    }
}
