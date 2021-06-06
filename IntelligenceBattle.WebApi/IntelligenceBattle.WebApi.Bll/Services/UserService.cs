using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
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

        public async Task DeleteSendQuestion(int id)
        {
            var a = await context.SendQuestions.FirstOrDefaultAsync(x => x.Id == id);
            context.Remove(a);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetUserRealId(int userId, int providerId)
        {
            var authProvider = await context.AuthorizationProviders.Include(x => x.AuthorizationProviderType)
                .FirstOrDefaultAsync(x => x.Id == providerId);
            var us = await context.UserSecurities.FirstOrDefaultAsync(x =>
                x.UserId == userId && x.AuthorizationCenterId ==
                authProvider.AuthorizationProviderType.AuthorizationProviderCenterId);
            if (us != null)
            {
                return us.RealId;
            }

            throw ExceptionFactory.SoftException(ExceptionEnum.UserNotFound, "UserNotFound");
        }

        public async Task<List<Notification>> GetNotification(int providerId)
        {
            return await context.Notifications
                .Include(x => x.Type)
                .Include(x => x.User)
                .ThenInclude(x => x.Lang)
                .Where(x => x.ProviderId == providerId)
                .ToListAsync();

        }

        public async Task DeleteNotification(int id)
        {
            context.Notifications.Remove(await context.Notifications.FirstOrDefaultAsync(x => x.Id == id));
            await context.SaveChangesAsync();

        }

        public async Task<List<Lang>> GetLangs()
        {
            return await context.Langs.ToListAsync();
        }

        public async Task SetUserLang(int langId, int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            user.LangId = langId;
            await context.SaveChangesAsync();
        }
    }
}
