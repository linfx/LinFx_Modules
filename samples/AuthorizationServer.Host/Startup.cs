using LinFx.Extensions.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityRole = LinFx.Extensions.Identity.Domain.IdentityRole;
using IdentityUser = LinFx.Extensions.Identity.Domain.IdentityUser;

namespace AuthorizationServer.Host
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
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                //.AddAspNetIdentity<IdentityUser>();
                .AddTestUsers(TestUsers.Users)
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddDeveloperSigningCredential();
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseSqlite(connectionString);
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseSqlite(connectionString);
                //    options.EnableTokenCleanup = true;
                //});

            services.AddAuthentication();

            services.AddControllersWithViews();
            services.AddRazorPages();
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

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
