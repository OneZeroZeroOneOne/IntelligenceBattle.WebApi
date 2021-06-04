using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutAnswer
    {

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<OutAnswerTranslation> Translations { get; set; }
    }
}
