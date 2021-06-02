using IntelligenceBattle.WebApi.Dal.Models.Out;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using IntelligenceBattle.WebApi.Bll.Services;
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
        public UserService userService;
        public IMapper mapperProfile;

        public UserController(IServiceProvider sP, UserService userS, IMapper mapper)
        {
            serviceProvider = sP;
            userService = userS;
            mapperProfile = mapper;
        }

        [HttpGet]
        public async Task<UserResponce> Get()
        {
            return mapperProfile.Map<UserResponce>(await userService.GetUser(UserId));
        }
    }
}
