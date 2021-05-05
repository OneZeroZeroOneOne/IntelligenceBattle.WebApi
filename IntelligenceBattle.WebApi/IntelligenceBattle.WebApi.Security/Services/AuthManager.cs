using IntelligenceBattle.WebApi.Dal.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.Services
{
    public class AuthManager
    {
        private PublicContext _context;
        public AuthManager(PublicContext publicContext)
        {
            _context = publicContext;
        }

        public bool CheckProvider():

    }
}
