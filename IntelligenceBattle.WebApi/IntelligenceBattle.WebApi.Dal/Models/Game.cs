using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class Game
    {
        public Game()
        {
            GameUsers = new HashSet<GameUser>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDatetime { get; set; }

        public virtual ICollection<GameUser> GameUsers { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
