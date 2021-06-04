using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutAnswerTranslation
    {
        public int AnswerId { get; set; }
        public int LangId { get; set; }
        public string Title { get; set; }
        public string LangCode { get; set; }
    }
}
