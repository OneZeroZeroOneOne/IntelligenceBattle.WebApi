using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutSearchGame
    {
        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
    }
}
