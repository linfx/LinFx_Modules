using IdentityServer4.EntityFramework.DbContexts;
using LinFx.Web.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Identity.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
                    var settings = services.GetService<IOptions<AppSettings>>();

                    new ApplicationDbContextSeed()
                        .SeedAsync(context, env, logger, settings)
                        .Wait();
                })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    var configuration = services.GetService<IConfiguration>();

                    new ConfigurationDbContextSeed()
                        .SeedAsync(context, configuration)
                        .Wait();
                })
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseKestrel()
                //.UseHealthChecks("/hc")
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                //.UseApplicationInsights()
                .Build();
    }
}

