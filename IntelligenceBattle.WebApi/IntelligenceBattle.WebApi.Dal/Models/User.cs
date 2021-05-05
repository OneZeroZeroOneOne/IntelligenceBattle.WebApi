using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class User
    {
        public User()
        {
            GameUsers = new HashSet<GameUser>();
            UserAnswers = new HashSet<UserAnswer>();
            UserSecurities = new HashSet<UserSecurity>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDatetime { get; set; }

        public virtual ICollection<GameUser> GameUsers { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual ICollection<UserSecurity> UserSecurities { get; set; }
    }
}
