using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Utilities.Exceptions
{
    public class ExceptionFactory
    {
        public static FriendlyException FriendlyException(ExceptionEnum code, string message)
        {
            return new FriendlyException((int)code, message);
        }

        public static SoftException SoftException(ExceptionEnum code, string message)
        {
            return new SoftException((int)code, message);
        }
    }
}
