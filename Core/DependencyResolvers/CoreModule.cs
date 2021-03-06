using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection servicecollection)
        {
            servicecollection.AddMemoryCache();
            servicecollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            servicecollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            servicecollection.AddSingleton<Stopwatch>();
        }

        
        
    }
}
