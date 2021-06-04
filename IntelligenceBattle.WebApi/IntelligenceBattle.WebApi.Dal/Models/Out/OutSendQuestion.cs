using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutSendQuestion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public int QuestionId { get; set; }
        public int GameId { get; set; }
        public OutQuestion Question { get; set; }
        public OutUser User { get; set; }
    }
}
