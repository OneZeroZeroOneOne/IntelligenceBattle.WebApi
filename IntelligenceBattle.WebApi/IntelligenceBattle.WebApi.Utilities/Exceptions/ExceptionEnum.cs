using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Utilities.Exceptions
{
    public enum ExceptionEnum
    {
        UserWithThisLoginAlreadyExist = 1,
        PasswordTooShort = 2,
        LoginTooShort = 3,
        SecurityKeyIsNull = 4,
        ProviderNotFound = 5,
        LoginIsAbsend = 6,
        UserWithLoginAlreadyExist = 7,
        ProviderKeyNotFound = 8,
    }
}
