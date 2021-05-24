using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Dal.Models.In;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Bll.Abstractions
{
    public interface IRegisterService
    {
        Task<User> RegisterUser(RegisterInModel registerInModel);
    }
}
