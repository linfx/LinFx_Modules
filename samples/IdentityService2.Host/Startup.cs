using IdentityServer4.Validation;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityService.Host
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly)));

            //Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            //IdentityServer4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddAspNetIdentity<ApplicationUser>()
                //.AddTestUsers(Config.GetUsers())
                .AddSecretValidator<PlainTextSharedSecretValidator>()
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });


            //认证
            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            //.AddJwtBearer(options =>
            //{
            //    //options.Audience = configuration["JwtBearer:Audience"];
            //    //options.Authority = configuration["JwtBearer:Authority"];
            //    options.RequireHttpsMetadata = false;
            //})
            //.AddIdentityCookies(options => { }); ;

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
