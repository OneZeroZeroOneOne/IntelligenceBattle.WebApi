using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using System;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class RegisterService
    {
        public PublicContext _context;
        public RegisterService(PublicContext publicContext)
        {
            _context = publicContext;
        }

        public async Task<User> RegisterUser(string login, string password, string name, string surname)
        {
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
