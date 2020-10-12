using CodebookManagement;
using Common.Base;
using CompanyManagement;
using Configuration.Extensions;
using Configuration.Services;
using Entity;
using FileManagement;
using Hangfire;
using Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Scheduling.Hubs;
using Scheduling.SelectList;
using Security.Extensions;
using SQLContext;
using System;
using System.IO;
using UserManagement;
using Web.Security.TokenProvider.Implementation;

namespace SchedulingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            Configuration.Bind(AppSettings.Instance);
            services.AddDependencies();
            services.AddSelectListDependencies();
            services.AddHangfireServer();
            services.AddLocalizationDependencies();
            services.AddConfigurationDependencies();
            services.AddCompanyManagementDependencies();
            services.AddCodebookDependencies();
            services.AddUserManagementDependencies();
            services.AddSQLContextDependencies();
            services.AddApiAuthentication();
            services.AddFileManagementDependencies();
            services.AddCors(options =>
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       //.AllowCredentials()
                )
            );
            services.AddSignalR();
            //services.AddCors();
            //services.AddSpaStaticFiles(config => { config.RootPath = "wwwroot"; });
            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DependencyInjectionResolver.Initialization(app.ApplicationServices);
            if (!string.IsNullOrEmpty(AppSettings.Instance.Config))
                Configuration.LoadDatabaseConfiguration(app.ApplicationServices.GetService<IConfigurationService>());
    
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"upload/temp")),
                RequestPath = new PathString("/upload/temp")
            });
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"assets/customIcons")),
            //    RequestPath = new PathString("/api/assets/customIcons")
            //});

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseJWTTokenProviderMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalHub>("signal");
            });
            if (env.IsDevelopment())
                app.UseHangfireDashboard();
        }
    }
}
