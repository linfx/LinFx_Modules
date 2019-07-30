using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityService.Host
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        //public static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args).Build()
        //        .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
        //        .MigrateDbContext<ConfigurationDbContext>((context, services) =>
        //        {
        //            var configuration = services.GetService<IConfiguration>();

        //            new ConfigurationDbContextSeed()
        //                .SeedAsync(context, configuration)
        //                .Wait();
        //        })
        //        .Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
