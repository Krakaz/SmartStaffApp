using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerService.Workers;
using WorkerService.Workers.Implementation;

namespace WorkerService
{
    public static class WorkersCollectionExtensions
    {
        public static IServiceCollection AddWorkersCollection(this IServiceCollection services)
        {
            services.AddScoped<ILoadInterviewDataService, LoadInterviewDataService>();

            return services;
        }
    }
}
