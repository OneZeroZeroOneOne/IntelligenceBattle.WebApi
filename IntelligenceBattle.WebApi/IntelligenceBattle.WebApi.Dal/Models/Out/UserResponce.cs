using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class UserResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangId { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
