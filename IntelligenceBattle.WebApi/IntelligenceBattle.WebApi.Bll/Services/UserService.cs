using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class UserService
    {
        public PublicContext context;
        public UserService(PublicContext publicContext)
        {
            context = publicContext;
        }

        public async Task<User> GetUser(int userId)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

    }
}
