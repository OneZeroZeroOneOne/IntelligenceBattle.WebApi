using System;
using System.Collections.Generic;
using System.Linq;
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
                throw ExceptionFactory.FriendlyException(ExceptionEnum.GameAlreadySearch, "GameAlreadySearch");
            }
            var game = await context.Games.FirstOrDefaultAsync(x => x.IsEnd == true);
            if (game != null)
            {
                throw ExceptionFactory.FriendlyException(ExceptionEnum.LastGameNotEnd, "LastGameNotEnd");
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
            context.SearchGames.Remove(new SearchGame
            {
                Id = searchId,
            });
            await context.SaveChangesAsync();
        }

        public async Task<List<GameType>> GetGameTypes()
        {

            return await context.GameTypes.Include(x => x.GameTypeTranslations).ThenInclude(x => x.Lang).ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.Include(x => x.CategoryTranslations).ThenInclude(x => x.Lang).ToListAsync();
        }
    }
}
