using IntelligenceBattle.WebApi.Dal.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Dal.Models.Out;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Security.Services;
using IntelligenceBattle.WebApi.Bll.Services;

namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public RegisterService _regService;
        public JwtTokenService _jwtTokenService;
        public RegisterController(RegisterService regService, JwtTokenService jwtTokenService)
        {
            _regService = regService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async Task<AuthorizationResponse> Register([FromBody] RegisterInModel registerInModel)
        {
            _regService.RegisterUser(registerInModel)
            _jwtTokenService.GetIdentity();
        }
    }
}
