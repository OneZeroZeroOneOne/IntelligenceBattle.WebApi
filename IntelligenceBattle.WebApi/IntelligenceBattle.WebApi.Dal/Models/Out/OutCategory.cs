using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<OutCategoryTranslation> Translations { get; set; }

    }
}
