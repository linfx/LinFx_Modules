using LinFx.Identity.Application;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.EntityFrameworkCore;
using LinFx.Identity.Web.Host.Menus;
using LinFx.UI.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LinFx.Identity.Web.Host
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            services.AddIdentityCore<ApplicationUser>()
                 .AddRoles<ApplicationRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddSignInManager()
                .AddDefaultTokenProviders();

            //Menus
            services.AddSingleton<IMenuManager, MenuManager>();
            services.Configure<NavigationOptions>(o =>
            {
                o.MenuContributors.Add(new IdentityMenuContributor());
                o.MenuContributors.Add(new Identity2MenuContributor());
            });

            //Permissions
            services.AddSingleton<IdentityPermissionDefinitionProvider>();

            services.AddLinFx()
                .AddAuthorization(o =>
                {
                    o.Permissions.DefinitionProviders.Add<IdentityPermissionDefinitionProvider>();
                });

            //认证
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(options => { });

            services.AddMvc()
                .AddMvcLocalization()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddApplicationPart(Assembly.Load("LinFx.Identity.Web"))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
