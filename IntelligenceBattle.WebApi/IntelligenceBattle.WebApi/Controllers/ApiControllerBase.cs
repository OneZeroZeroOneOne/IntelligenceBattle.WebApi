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
        public int AuthCenterId => int.Parse(User.Claims.First(x => x.Type == "AuthCenterId").Value);

        public int ProviderId => int.Parse(User.Claims.First(x => x.Type == "ProviderId").Value);

        public int UserId => int.Parse(User.Claims.First(x => x.Type == "UserId").Value);

    }
}
