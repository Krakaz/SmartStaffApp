using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddWorkerCollection(this IHostBuilder builder)
        {
            return builder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsumeScopedServiceHostedService>();
            });
        }
    }
}
