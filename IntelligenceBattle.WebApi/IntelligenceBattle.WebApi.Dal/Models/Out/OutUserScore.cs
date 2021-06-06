using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Dal.Models.Out
{
    public class OutUserScore
    {
        public OutUser User { get; set; }
        public int Score { get; set; }
    }
}
