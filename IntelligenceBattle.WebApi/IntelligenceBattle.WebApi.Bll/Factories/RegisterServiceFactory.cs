using IntelligenceBattle.WebApi.Bll.Abstractions;
using IntelligenceBattle.WebApi.Bll.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligenceBattle.WebApi.Bll.Factories
{
    public static class RegisterServiceFactory
    {
        public static Dictionary<string, Type> services = new Dictionary<string, Type>()
        {
            {"bearer", typeof(RegisterServiceUI) },
            {"telegram", typeof(RegisterServiceTelegram) },
        };
        public static IRegisterService GetRegisterServiceCenter(string authCenter, string token, IServiceProvider serviceProvider)
        {
            if(services.TryGetValue(authCenter, out var serviceType))
            {
                return (IRegisterService)ActivatorUtilities.CreateInstance(serviceProvider, serviceType, token);
            }
            return null;
        }
    }
}
