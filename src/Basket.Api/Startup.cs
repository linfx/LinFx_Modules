using LinFx.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Basket.Api.Services;
using LinFx.Extensions.EventBus.RabbitMQ;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Basket
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
            services
                .AddCustomMVC(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomOptions(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddIntegrationServices(Configuration)
                .AddCustomSwagger();

            services.AddLinFx()
                .AddDistributedRedisCache(options =>
                {
                    options.InstanceName = "shopfx_basket:";
                    options.Configuration = Configuration.GetConnectionString("ReidsConnection");
                })
                .AddEventBus(options =>
                {
                    options.Durable = true;
                    options.BrokerName = "shopfx_event_bus";
                    options.QueueName = "shopfx_process_queue";
                    options.ConfigureEventBus = (fx, builder) => builder.UseRabbitMQ(fx, x =>
                    {
                        x.Host = "14.21.34.85";
                        x.UserName = "admin";
                        x.Password = "admin.123456";
                    });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //ConfigureAuth
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"http://localhost:5103/swagger/v1/swagger.json", "Basket.API v1");
              });
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHealthChecks(checks =>
            //{
            //    var minutes = 1;
            //    if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutesParsed))
            //    {
            //        minutes = minutesParsed;
            //    }
            //    checks.AddSqlCheck("CatalogDb", configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));

            //    var accountName = configuration.GetValue<string>("AzureStorageAccountName");
            //    var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");
            //    if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            //    {
            //        checks.AddAzureBlobStorageCheck(accountName, accountKey);
            //    }
            //});

            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<CatalogSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:5105";
                options.RequireHttpsMetadata = false;
                options.Audience = "basket";
            });

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IBasketRepository, RedisBasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "ShopFx - Basket HTTP API",
                    Version = "v1",
                    Description = "The Basket Microservice HTTP API.",
                });
            });
            return services;
        }
    }
}