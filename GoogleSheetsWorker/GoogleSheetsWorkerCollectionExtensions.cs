using Microsoft.Extensions.DependencyInjection;
using GoogleSheetsWorker.Services;
using GoogleSheetsWorker.Services.Implementation;

namespace GoogleSheetsWorker
{
    public static class GoogleSheetsWorkerCollectionExtensions
    {
        public static IServiceCollection AddGoogleSheetsWorkerCollection(this IServiceCollection services)
        {
            services.AddScoped<INewStaffService, NewStaffService>();

            return services;
        }
    }
}
