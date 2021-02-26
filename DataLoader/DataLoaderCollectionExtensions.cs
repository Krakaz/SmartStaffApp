using DataLoader.Maketalents.Services;
using DataLoader.Maketalents.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DataLoader
{
    public static class DataLoaderCollectionExtensions
    {
        public static IServiceCollection AddDataLoaderCollection(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMaketalentsService, MaketalentsService>();

            return services;
        }
    }
}
