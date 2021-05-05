using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Utilities.Exceptions
{
    public class SoftException: Exception
    {
        public int exceptionCode;
        public string exceptionMessage;
        public SoftException(int code, string message) : base(message)
        {
            exceptionMessage = message;
            exceptionCode = code;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { exceptionCode, Message = exceptionMessage });
        }
    }
}
