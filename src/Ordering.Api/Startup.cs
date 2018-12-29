using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LinFx.Extensions.EventBus.Abstractions;
using LinFx.Extensions.EventBus.RabbitMQ;
using LinFx.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Api.Infrastructure;
using Ordering.API.Application.IntegrationEvents;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Application.Services;
using Ordering.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace Ordering.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddApplicationInsights(Configuration)
                .AddHealthChecks(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddCustomConfiguration(Configuration)
                .AddEventBus(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddCustomSwagger(Configuration);

            services.AddLinFx()
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

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());
            //container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
            return new AutofacServiceProvider(container.Build());
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddAzureWebAppDiagnostics();
            //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

            //var pathBase = Configuration["PATH_BASE"];
            //if (!string.IsNullOrEmpty(pathBase))
            //{
            //    loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
            //    app.UsePathBase(pathBase);
            //}

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
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Ordering.API V1");
                   //c.OAuthClientId("orderingswaggerui");
                   //c.OAuthAppName("Ordering Swagger UI");
               });

            ConfigureEventBus(app);
        }


        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, UserCheckoutAcceptedIntegrationEventHandler>();
            eventBus.Subscribe<GracePeriodConfirmedIntegrationEvent, IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>>();
            eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>>();
            eventBus.Subscribe<OrderStockRejectedIntegrationEvent, IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>>();
            eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>>();
            eventBus.Subscribe<OrderPaymentSuccededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSuccededIntegrationEvent>>();
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddApplicationInsightsTelemetry(configuration);
            //var orchestratorType = configuration.GetValue<string>("OrchestratorType");

            //if (orchestratorType?.ToUpper() == "K8S")
            //{
            //    // Enable K8s telemetry initializer
            //    services.EnableKubernetes();
            //}
            //if (orchestratorType?.ToUpper() == "SF")
            //{
            //    // Enable SF telemetry initializer
            //    services.AddSingleton<ITelemetryInitializer>((serviceProvider) =>
            //        new FabricTelemetryInitializer());
            //}

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
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

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHealthChecks(checks =>
            //{
            //    var minutes = 1;
            //    if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutesParsed))
            //    {
            //        minutes = minutesParsed;
            //    }
            //    checks.AddSqlCheck("OrderingDb", configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));
            //});

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderingContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                                     sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(OrderingContext).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            });

            //services.AddDbContext<IntegrationEventLogContext>(options =>
            //{
            //    options.UseSqlServer(configuration["ConnectionString"],
            //                         sqlServerOptionsAction: sqlOptions =>
            //                         {
            //                             sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            //                             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            //                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //                         });
            //});

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "ShopFx - Ordering HTTP API",
                    Version = "v1",
                    Description = "The Ordering Service HTTP API",
                });

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                //    TokenUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                //    Scopes = new Dictionary<string, string>()
                //    {
                //        { "orders", "Ordering API" }
                //    }
                //});

                //options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            //services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
            //    sp => (DbConnection c) => new IntegrationEventLogService(c));
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            //services.Configure<OrderingSettings>(configuration);
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

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

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
                options.Audience = "orders";
            });

            return services;
        }
    }
}
