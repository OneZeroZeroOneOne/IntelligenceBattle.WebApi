using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedDatetime { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
