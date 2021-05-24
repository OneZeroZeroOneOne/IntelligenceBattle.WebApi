using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.In
{
    public class RegisterInModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public int? RealId { get; set; }
        public int ProviderId { get; set; }
    }
}
