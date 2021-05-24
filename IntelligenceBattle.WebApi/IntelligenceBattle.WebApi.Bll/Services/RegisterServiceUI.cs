using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using System;
using IntelligenceBattle.WebApi.Dal.Models.In;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class RegisterServiceUI
    {
        public PublicContext _context;
        public RegisterServiceUI(PublicContext publicContext)
        {
            _context = publicContext;
        }

        public async Task<User> RegisterUser(RegisterInModel registerInModel, string pT)
        {
            var provider = await _context.AuthorizationProviders.FirstOrDefaultAsync(x => x.Id == registerInModel.ProviderId);
            if (provider == null) throw ExceptionFactory.FriendlyException(ExceptionEnum.ProviderNotFound, "provider not found");
            if (provider.Id == 1)
            {
                if (registerInModel.Login == null) throw ExceptionFactory.FriendlyException(ExceptionEnum.LoginIsAbsend, "login is absend")
                var userSec = await _context.UserSecurities.FirstOrDefaultAsync(x => x.Login == registerInModel.Login);
                if (userSec) throw ExceptionFactory.FriendlyException(ExceptionEnum.UserWithLoginAlreadyExist, "user with this login already exist");
                var newUserSec = new UserSecurity
                {

                }
            }

            var userSec = await _context.UserSecurities.Where(x => x.Login == login).Include(x => x.User).FirstOrDefaultAsync();
            if (userSec != null)
            {
                throw ExceptionFactory.FriendlyException(ExceptionEnum.UserWithThisLoginAlreadyExist, "user with this login already exist");
            }
            if(login.Length < 4)
            {
                throw ExceptionFactory.FriendlyException(ExceptionEnum.LoginTooShort, "login too short");
            }
            if(password.Length < 8)
            {
                throw ExceptionFactory.FriendlyException(ExceptionEnum.PasswordTooShort, "password too short");
            }
            var user = new User()
            {
                Name = name,
                Surname = surname,
                CreatedDatetime = DateTime.Now
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var newUserSec = new UserSecurity()
            {
                UserId = user.Id,
                Login = login,
                Password = password,
            };
            await _context.UserSecurities.AddAsync(newUserSec);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
