using IntelligenceBattle.WebApi.Bll.Factories;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Dal.Models.Out;
using IntelligenceBattle.WebApi.Security.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public JwtTokenService _jwtTokenService;
        public IServiceProvider serviceProvider;
        public RegisterController(JwtTokenService jwtTokenService, IServiceProvider sP)
        {
            _jwtTokenService = jwtTokenService;
            serviceProvider = sP;
        }

        [HttpPost]
        public async Task<AuthorizationResponse> Register([FromBody] RegisterInModel registerInModel)
        {
            var isProviderTokenExist = Request.Headers.TryGetValue("ProviderToken", out var providerToken);
            if (isProviderTokenExist)
            {
                var regService = RegisterServiceFactory.GetRegisterServiceProvider(registerInModel.ProviderId, serviceProvider);
                regService.RegisterUser();


                _jwtTokenService.GetIdentity();
            }
            //return Ok();
        }
    }
}
