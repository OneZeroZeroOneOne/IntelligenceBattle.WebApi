using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class GameType
    {
        public GameType()
        {
            SearchGames = new HashSet<SearchGame>();
        }

        public int Id { get; set; }
        public string Tittle { get; set; }
        public int PlayerCount { get; set; }

        public virtual ICollection<SearchGame> SearchGames { get; set; }
    }
}
