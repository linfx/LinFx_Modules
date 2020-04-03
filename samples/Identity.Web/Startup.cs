using Identity.Web.Menus;
using LinFx.Extensions.Identity.Data;
using LinFx.Extensions.Identity.Models;
using LinFx.Extensions.Identity.Permissions;
using LinFx.Extensions.UI.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Web
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

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            services.AddIdentityCore<User>()
                 .AddRoles<Role>()
                 .AddEntityFrameworkStores<IdentityDbContext>()
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

            services.AddControllersWithViews()
                .AddMvcLocalization()
                //.AddApplicationPart(Assembly.Load("LinFx.Identity.Web"))
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
