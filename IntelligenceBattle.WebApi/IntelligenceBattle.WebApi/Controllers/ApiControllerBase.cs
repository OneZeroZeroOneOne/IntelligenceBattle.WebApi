using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        string ProviderId => User.Claims.First(x => x.Type == "ProvideId").Value;

    }
}
