using System;
using System.Collections.Generic;

#nullable disable

namespace IntelligenceBattle.WebApi.Dal.Models
{
    public partial class AuthorizationProvider
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int? AuthorizationProviderTypeId { get; set; }

        public virtual AuthorizationProviderType AuthorizationProviderType { get; set; }
    }
}
