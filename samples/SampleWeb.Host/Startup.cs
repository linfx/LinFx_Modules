using LinFx.Extensions.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleWeb.Host.Menus;

namespace SampleWeb.Host
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

            //services.AddDbContext<PermissionManagementDbContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<TenantManagementDbContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            ////Identity
            //services.AddIdentityCore<User>()
            //     .AddRoles<Role>()
            //     .AddEntityFrameworkStores<IdentityDbContext>()
            //     .AddSignInManager()
            //    .AddDefaultTokenProviders();

            services.AddLinFx()
                .AddHttpContextPrincipalAccessor()
                .AddNavigation(options =>
                {
                    options.MenuContributors.Add(new IdentityMenuContributor());
                    options.MenuContributors.Add(new Identity2MenuContributor());
                });

            ////Permissions
            //services.AddSingleton<IdentityPermissionDefinitionProvider>();

            //services.AddLinFx()
            //    .AddAuthorization(o =>
            //    {
            //        o.Permissions.DefinitionProviders.Add<IdentityPermissionDefinitionProvider>();
            //    });

            ////хож╓
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //})
            //.AddIdentityCookies(options => { });

            //services.AddControllersWithViews()
            //    .AddMvcLocalization()
            //    .AddApplicationPart(Assembly.Load("LinFx.Module.Account.UI"))
            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleWeb", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
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

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleWeb");
            });

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