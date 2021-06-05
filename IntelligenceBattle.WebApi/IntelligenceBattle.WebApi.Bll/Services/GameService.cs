using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class GameService
    {
        public readonly PublicContext context;
        public GameService(PublicContext publicContext)
        {
            context = publicContext;
        }


        public async Task<SearchGame> StartSearchGame(InSearchGame inSearchGame, int userId, int providerId)
        {
            var lastSearcGame = await context.SearchGames.FirstOrDefaultAsync(x => x.UserId == userId);
            if (lastSearcGame != null)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.GameAlreadySearch, "GameAlreadySearch");
            }
            var game = await context.Games.FirstOrDefaultAsync(x => x.IsEnd == false);
            if (game != null)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.LastGameNotEnd, "LastGameNotEnd");
            }
            var newSearchGame = new SearchGame
            {
                GameTypeId = inSearchGame.GameTypeId,
                CategoryId = inSearchGame.CategoryId,
                ProviderId = providerId,
                UserId = userId
            };
            await context.SearchGames.AddAsync(newSearchGame);
            await context.SaveChangesAsync();
            return newSearchGame;
        }

        public async Task StopSearchGame(int searchId)
        {
            var s = await context.SearchGames.FirstOrDefaultAsync(x => x.Id == searchId);
            if (s != null)
            {
                context.SearchGames.Remove(s);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task StopAllSearchGame(int userId)
        {
            var s = await context.SearchGames.FirstOrDefaultAsync(x => x.UserId == userId);
            if (s != null)
            {
                context.SearchGames.Remove(s);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<GameType>> GetGameTypes()
        {

            return await context.GameTypes.Include(x => x.GameTypeTranslations).ThenInclude(x => x.Lang).ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.Include(x => x.CategoryTranslations).ThenInclude(x => x.Lang).ToListAsync();
        }


        public async Task<bool> UserAnswer(InUserAnswer inUserAnswer, int userId)
        {
            var gameQuestion = await context.GameQuestions
                .FirstOrDefaultAsync(x => x.QuestionId == inUserAnswer.QuestionId && x.GameId == inUserAnswer.GameId);
            if (gameQuestion.IsCurrent == true)
            {
                var newUa = new UserAnswer
                {
                    AnswerId = inUserAnswer.AnswerId,
                    GameId = inUserAnswer.GameId,
                    QuestionId = inUserAnswer.QuestionId,
                    UserId = userId,
                };
                await context.UserAnswers.AddAsync(newUa);
                await context.SaveChangesAsync();
                var answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == inUserAnswer.AnswerId);
                return answer.IsTrue;
            }
            throw ExceptionFactory.SoftException(ExceptionEnum.QuestionNotCurrent, "QuestionNotCurrent");
        }
    }
}
