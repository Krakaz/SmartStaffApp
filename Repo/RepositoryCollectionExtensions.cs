using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repo.Models;
using Repo.Services;
using Repo.Services.Implementation;

namespace Repo
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepositoryCollection(this IServiceCollection services, string defaultConnection)
        {
            services.AddDbContext<RepoContext>(options => options.UseSqlServer(defaultConnection,
                providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<INotificationEmailsService, NotificationEmailsService>();
            services.AddScoped<IApplicantService, ApplicantService>();

            return services;
        }
    }
}
