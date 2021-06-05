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
        AuthCenterNotFound = 9,
        InvalidLoginOrPassword = 10,
        LoginExist = 11,
        ProviderTokenAbsent = 12,
        InvalidProviderTokenFormat = 13,
        RealIdIsAbsent = 14,
        UserNotFound = 15,
        GameAlreadySearch = 16,
        LastGameNotEnd = 17,
        AccessDenied = 18,
        QuestionNotCurrent = 19,
    }
}
