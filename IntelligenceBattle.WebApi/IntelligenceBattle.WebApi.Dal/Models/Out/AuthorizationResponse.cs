using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class AuthorizationResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }

    }
}
