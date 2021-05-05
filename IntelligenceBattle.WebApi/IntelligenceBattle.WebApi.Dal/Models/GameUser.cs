using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class GameUser
    {
        public int GameId { get; set; }
        public int UserId { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
