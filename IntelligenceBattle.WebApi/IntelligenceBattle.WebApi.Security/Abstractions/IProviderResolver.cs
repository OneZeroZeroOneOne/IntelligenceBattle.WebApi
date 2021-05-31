using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.Abstractions
{
    public interface IProviderResolver
    {
        Task<List<Claim>> GetClaims();
    }
}
