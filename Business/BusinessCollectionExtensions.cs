using Business.Services;
using Business.Services.Implementation;
using DataLoader;
using GoogleSheetsWorker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repo;

namespace Business
{
    public static class BusinessCollectionExtensions
    {
        public static IServiceCollection BusinessCollection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddRepositoryCollection(configuration.GetConnectionString("DefaultConnection"));
            services.AddDataLoaderCollection();
            services.AddGoogleSheetsWorkerCollection();

            services.AddScoped<IInterviewInformationService, InterviewInformationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IStaffService, StaffService>();

            return services;
        }
    }
}
