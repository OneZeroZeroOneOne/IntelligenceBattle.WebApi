using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Abstractions;
using IntelligenceBattle.WebApi.Security.Configs;
using IntelligenceBattle.WebApi.Security.ProviderResolvers;
using IntelligenceBattle.WebApi.Security.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.Handlers
{
    public class CustomAuthHandler : AuthenticationHandler<AuthOptions>
    {
        public IServiceProvider _serviceProvider;

        public CustomAuthHandler(
            IOptionsMonitor<AuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IServiceProvider serviceProvider)
            : base(options, logger, encoder, clock)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("authorization header is absent");
            //var scope = serviceProvider.CreateScope
            string token = Request.Headers["Authorization"];
            IProviderResolver resolver = ProviderResolverFactory.GetResolver(token, _serviceProvider);
            if(resolver == null)
            {
                return AuthenticateResult.Fail("unknown provider");
            }
            try
            {
                var claims = resolver.GetClaims();
                if (claims.Count > 0)
                {
                    return AuthenticateResult.Fail("unauthorized");
                }
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                
                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("unauthorized");
            }
        }


    }

    class ProviderResolverFactory
    {
        public static IProviderResolver GetResolver(string token, IServiceProvider serviceProvider)
        {
            if(token.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return (IProviderResolver)ActivatorUtilities.CreateInstance(serviceProvider, typeof(UIProviderResolver), token);
            }
            //(token.StartsWith("telegram", StringComparison.OrdinalIgnoreCase))
            else if(token.StartsWith("telegram", StringComparison.OrdinalIgnoreCase))
            {
                return (IProviderResolver)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TelegramProviderResolver), token);
            }
            return null;
        }
    }
}
