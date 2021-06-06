using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutNotification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TypeId { get; set; }
        public int ProviderId { get; set; }
        public int UserId { get; set; }
        public string TypeTittle { get; set; }
        public OutUser User { get; set; }
    }
}
