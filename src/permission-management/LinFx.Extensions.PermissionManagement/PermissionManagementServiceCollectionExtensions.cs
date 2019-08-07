using LinFx.Extensions.PermissionManagement;
using LinFx.Extensions.PermissionManagement.Application;
using LinFx.Security.Authorization.Permissions;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PermissionManagementServiceCollectionExtensions
    {
        public static IServiceCollection AddLinFxPermissionManagement(this IServiceCollection services)
        {
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddSingleton<PermissionManager>();

            //services.AddHttpContextAccessor();
            //services.AddTransient<IHttpContextPrincipalAccessor, HttpContextPrincipalAccessor>();

            //services.AddSingleton<IPermissionChecker, PermissionChecker>();
            services.AddSingleton<IPermissionDefinitionContext, PermissionDefinitionContext>();
            services.AddSingleton<IPermissionDefinitionManager, PermissionDefinitionManager>();

            return services;
        }
    }
}
