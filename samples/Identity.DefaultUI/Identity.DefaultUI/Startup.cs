using LinFx.Identity.Application;
using LinFx.Identity.Application.Services;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.EntityFrameworkCore;
using LinFx.Security.Authorization.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DefaultUI
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>()
                 .AddRoles<ApplicationRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddTransient<ApplicationUserService>();

            services.AddSingleton<IdentityPermissionDefinitionProvider>();
            services.AddSingleton<IPermissionDefinitionContext, PermissionDefinitionContext>();
            services.AddSingleton<IPermissionDefinitionManager, PermissionDefinitionManager>();

            services.Configure<PermissionOptions>(o =>
            {
                o.DefinitionProviders.Add<IdentityPermissionDefinitionProvider>();
            });

            services.AddLocalization();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });

            services.AddMvc()
                .AddMvcLocalization()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
