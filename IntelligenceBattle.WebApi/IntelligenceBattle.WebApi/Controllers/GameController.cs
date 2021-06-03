using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IntelligenceBattle.WebApi.Bll.Services;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Dal.Models.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ApiControllerBase
    {
        public GameService gameService;
        public IMapper mapperProfile;
        public GameController(GameService gS, IMapper mapper)
        {
            gameService = gS;
            mapperProfile = mapper;
        }

        [HttpPost]
        [Route("SearchGame")]
        public async Task<OutSearchGame> StartSearchGame([FromBody] InSearchGame inSearchGame)
        {
            var searchGame = await gameService.StartSearchGame(inSearchGame, UserId, ProviderId);
            return mapperProfile.Map<OutSearchGame>(searchGame);
        }

        [HttpDelete]
        [Route("SearchGame")]
        public async Task<IActionResult> StopSearchGame([FromQuery] int searchId)
        {
            await gameService.StopSearchGame(searchId);
            return Ok();
        }

        [HttpGet]
        [Route("GameType")]
        public async Task<List<OutGameType>> GetGameTypes()
        {
            var gameTypes = await gameService.GetGameTypes();
            return mapperProfile.Map<List<OutGameType>>(gameTypes);
        }

        [HttpGet]
        [Route("Category")]
        public async Task<List<OutCategory>> GetCategories()
        {
            var categories = await gameService.GetCategories();
            return mapperProfile.Map<List<OutCategory>>(categories);
        }

    }
}
