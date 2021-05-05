using IntelligenceBattle.WebApi.Dal.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IntelligenceBattle.WebApi.Dal.Models.Out;
using IntelligenceBattle.WebApi.Dal.Models.In;

namespace IntelligenceBattle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public PublicContext _context;
        public RegisterController(PublicContext publicContext)
        {
            _context = publicContext;
        }

        [HttpPost]
        public async Task<AuthorizationResponse> Register([FromBody] RegisterInModel registerInModel)
        {
            
        }
    }
}
