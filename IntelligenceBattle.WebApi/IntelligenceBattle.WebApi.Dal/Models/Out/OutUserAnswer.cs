using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutUserAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int AnswerId { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int QuestionId { get; set; }
    }
}
