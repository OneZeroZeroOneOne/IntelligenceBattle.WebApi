using System.Collections.Generic;
using System.Security.Claims;

namespace IntelligenceBattle.WebApi.Security.Abstractions
{
    public interface IProviderResolver
    {
        List<Claim> GetClaims();
    }
}
