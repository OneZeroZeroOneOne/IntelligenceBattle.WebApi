using System;
using System.Collections.Generic;
using System.Linq;
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
            return await context.Users.Include(x => x.Lang).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<List<SendQuestion>> GetSendQuestion(int providerId)
        {
            var a = await context.SendQuestions
                .Include(x => x.User)
                .Include(x => x.Question)
                .ThenInclude(x => x.QuestionTranslations)
                .ThenInclude(x => x.Lang)
                .Include(x => x.Question)
                .ThenInclude(x => x.Answers)
                .ThenInclude(x => x.AnswerTranslations)
                .ThenInclude(x => x.Lang)
                .Include(x => x.User)
                .ThenInclude(x => x.Lang)
                .Where(x => x.ProviderId == providerId)
                .ToListAsync();
            return a;
        }

    }
}
