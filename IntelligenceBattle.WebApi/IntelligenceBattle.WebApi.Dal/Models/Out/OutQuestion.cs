using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int CategoryId { get; set; }
        public string MediaUrl { get; set; }
        public List<OutQuestionTranslation> Translations { get; set; }
        public List<OutAnswer> Answers { get; set; }
    }
}
