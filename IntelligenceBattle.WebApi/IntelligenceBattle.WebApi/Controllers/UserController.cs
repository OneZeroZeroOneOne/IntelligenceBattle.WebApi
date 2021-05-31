using IntelligenceBattle.WebApi.Dal.Models.Out;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiControllerBase
    {
        public IServiceProvider serviceProvider;

        public UserController(IServiceProvider sP)
        {
            serviceProvider = sP;
        }

        [HttpGet]
        public async Task<UserResponce> Get()
        {
            var a = UserId;
            return new();
        }
    }
}
