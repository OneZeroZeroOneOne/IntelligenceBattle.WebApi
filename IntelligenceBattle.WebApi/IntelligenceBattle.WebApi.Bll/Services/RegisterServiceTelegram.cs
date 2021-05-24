using IntelligenceBattle.WebApi.Bll.Abstractions;
using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Dal.Models.In;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Bll.Services
{
    public class RegisterServiceTelegram : IRegisterService
    {
        private readonly PublicContext _context;
        public RegisterServiceTelegram(PublicContext publicContext)
        {
            _context = publicContext;
        }

        public async Task<User> RegisterUser(RegisterInModel registerInModel, string pT)
        {
            var provider = await _context.AuthorizationProviders.FirstOrDefaultAsync(x => x.Key == pT);
            if (provider == null)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ProviderKeyNotFound, "provider key not found");
            }
        }
    }
}
