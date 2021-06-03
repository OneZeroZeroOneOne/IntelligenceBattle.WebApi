using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutGameType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PlayerCount { get; set; }
        public List<OutGameTypeTranslation> Translations { get; set; }
    }
}
