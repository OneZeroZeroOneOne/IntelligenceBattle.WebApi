using IntelligenceBattle.WebApi.Bll.Factories;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Dal.Models.Out;
using IntelligenceBattle.WebApi.Security.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Utilities.Exceptions;

namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public JwtTokenService jwtTokenService;
        public IServiceProvider serviceProvider;
        public RegisterController(JwtTokenService jTokenService, IServiceProvider sP)
        {
            jwtTokenService = jTokenService;
            serviceProvider = sP;
        }

        [HttpPost]
        public async Task<AuthorizationResponse> Register([FromBody] RegisterInModel registerInModel)
        {
            var isProviderTokenExist = Request.Headers.TryGetValue("ProviderToken", out var providerToken);
            if (isProviderTokenExist)
            {
                var providerTokenComponents = providerToken.ToString().Split(" ");
                if (providerTokenComponents.Length == 2)
                {
                    var regService = RegisterServiceFactory.GetRegisterServiceCenter(providerTokenComponents[0], providerTokenComponents[1], serviceProvider);
                    if (regService != null)
                    {
                        var user = await regService.RegisterUser(registerInModel);
                        var token = jwtTokenService.GenerateToken(regService.GetAuthCenterName(),jwtTokenService.GetIdentity(user.Id,
                            user.UserSecurities.First().AuthorizationCenterId,
                            await regService.GetProviderId()));
                        return new AuthorizationResponse
                        {
                            Name = user.Name,
                            UserId = user.Id,
                            Token = token,
                        };
                    }
                    else
                    {
                        throw ExceptionFactory.SoftException(ExceptionEnum.AuthCenterNotFound, "AuthCenterNotFound");
                    }
                }
                else
                {
                    throw ExceptionFactory.SoftException(ExceptionEnum.InvalidProviderTokenFormat, "InvalidProviderTokenFormat");
                }
                
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ProviderTokenAbsent, "ProviderToken");
            }
        }
    }
}
