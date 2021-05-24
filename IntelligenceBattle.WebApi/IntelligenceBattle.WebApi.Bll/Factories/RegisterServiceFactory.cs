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
        public static Dictionary<int, Type> services = new Dictionary<int, Type>()
        {
            {1, typeof(RegisterServiceUI) },
            {2, typeof(RegisterServiceTelegram) },
        };
        public static IRegisterService GetRegisterServiceProvider(int providerId, IServiceProvider serviceProvider)
        {
            if(services.TryGetValue(providerId, out var serviceType))
            {
                return (IRegisterService)ActivatorUtilities.CreateInstance(serviceProvider, serviceType);
            }
            return null;
        }
    }
}
