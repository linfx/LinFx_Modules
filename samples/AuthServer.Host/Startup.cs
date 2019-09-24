using IdentityServer4.EntityFramework.DbContexts;
using LinFx.Extensions.Identity.Domain;
using LinFx.Extensions.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthServer.Host.Helpers;

namespace AuthServer.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureRootConfiguration(Configuration);

            // Add DbContext for Asp.Net Core Identity
            services.AddIdentityDbContext<IdentityDbContext>(Configuration, Environment);

            services
                .AddLinFx()
                .AddEmail(options =>
                {
                    options.Host = "133";
                    options.Login = "admin";
                    options.Password = "123456";
                });

            // Add services for authentication, including Identity model, IdentityServer4 and external providers
            services.AddAuthenticationServices<ConfigurationDbContext, PersistedGrantDbContext, IdentityDbContext, IdentityUser, IdentityRole>(Configuration);

            // Add all dependencies for Asp.Net Core Identity in MVC - these dependencies are injected into generic Controllers
            // Including settings for MVC and Localization
            // If you want to change primary keys or use another db model for Asp.Net Core Identity:
            services.AddMvcWithLocalization<IdentityUser, string>();

            // Add authorization policies for MVC
            services.AddAuthorizationPolicies();

            // 添加健康检测
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSecurityHeaders();
            app.UseIdentityServer();
            app.UseMvcLocalizationServices();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
