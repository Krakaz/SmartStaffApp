using DataLoader.Maketalents.Services;
using DataLoader.Maketalents.Services.Implementation;
using DataLoader.MyTeam.Services;
using DataLoader.MyTeam.Services.Implementation;
using DataLoader.Pozdravlala.Services;
using DataLoader.Pozdravlala.Services.Implementation;
using DataLoader.Timepad.Services;
using DataLoader.Timepad.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DataLoader
{
    public static class DataLoaderCollectionExtensions
    {
        public static IServiceCollection AddDataLoaderCollection(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMaketalentsService, MaketalentsService>();
            services.AddScoped<ITimePadEventService, TimePadEventService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPozdravlalaService, PozdravlalaService>();

            return services;
        }
    }
}
