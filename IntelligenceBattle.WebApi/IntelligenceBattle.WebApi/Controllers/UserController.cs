using IntelligenceBattle.WebApi.Dal.Models.Out;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IntelligenceBattle.WebApi.Bll.Services;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
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
        public async Task<OutUser> Get()
        {
            return mapperProfile.Map<OutUser>(await userService.GetUser(UserId));
        }

        [HttpGet]
        [Route("SendQuestion")]
        public async Task<List<OutSendQuestion>> GetSendQuestions()
        {
            if (UserId == 1)
            {
                return mapperProfile.Map<List<OutSendQuestion>>(await userService.GetSendQuestion(ProviderId));
            }
            throw ExceptionFactory.SoftException(ExceptionEnum.AccessDenied,"access denied");
        }

        [HttpDelete]
        [Route("SendQuestion")]
        public async Task<IActionResult> DeleteSendQuestions([FromQuery] int sendQuestionId)
        {
            if (UserId == 1)
            {
                await userService.DeleteSendQuestion(sendQuestionId);
                return Ok();;
            }
            throw ExceptionFactory.SoftException(ExceptionEnum.AccessDenied, "access denied");
        }


        [HttpGet]
        [Route("UserRealId")]
        public async Task<OutUserRealId> GetUserRealId([FromQuery] int userId)
        {
            if (UserId == 1)
            {
                return new OutUserRealId(){
                    RealId = await userService.GetUserRealId(userId, ProviderId)
                };
            }
            throw ExceptionFactory.SoftException(ExceptionEnum.AccessDenied, "access denied");
        }
    }
}
