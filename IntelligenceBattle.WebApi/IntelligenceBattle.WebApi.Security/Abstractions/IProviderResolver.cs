using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.Abstractions
{
    public interface IProviderResolver
    {
        bool Check(string token);
    }
}
