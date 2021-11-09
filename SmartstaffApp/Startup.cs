using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataLoader;
using GoogleSheetsWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repo;
using SmartstaffApp.Logger;
using SmartstaffApp.Services;
using SmartstaffApp.Services.Implementation;

namespace SmartstaffApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();

            services.BusinessCollection(Configuration);

            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IApplicantsService, ApplicantsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSwaggerGen();
            services.AddHttpClient();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathLogger = Path.Combine(Directory.GetCurrentDirectory(), "logs", DateTime.Now.Date.ToString("yyyyMMdd") + "logger" + ".txt");
            loggerFactory.AddFile(pathLogger);
            var logger = loggerFactory.CreateLogger("FileLogger");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartstaffApp API V0.7");
                c.RoutePrefix = "api";//string.Empty;
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                // определение маршрутов
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
