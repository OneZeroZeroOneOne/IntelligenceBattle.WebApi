using IntelligenceBattle.WebApi.Security.Abstractions;
using IntelligenceBattle.WebApi.Security.Configs;
using IntelligenceBattle.WebApi.Security.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.Handlers
{
    public class CustomAuthHandler : AuthenticationHandler<AuthOptions>
    {
        public AuthManager authenticationManager;
        public CustomAuthHandler(
            IOptionsMonitor<AuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            AuthManager customAuthenticationManager)
            : base(options, logger, encoder, clock)
        {
            authenticationManager = customAuthenticationManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("absent authorization header");
            string token = Request.Headers["Authorization"];


        }


    }

    class ProviderResolverFactory
    {
        public static IProviderResolver GetResolver(string token)
        {
            if(token.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return new 
            }
        }
    }
}
