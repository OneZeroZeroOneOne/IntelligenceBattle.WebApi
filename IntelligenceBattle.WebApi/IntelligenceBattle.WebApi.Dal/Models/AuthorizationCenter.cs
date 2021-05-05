using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class AuthorizationCenter
    {
        public AuthorizationCenter()
        {
            UserSecurities = new HashSet<UserSecurity>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<UserSecurity> UserSecurities { get; set; }
    }
}
