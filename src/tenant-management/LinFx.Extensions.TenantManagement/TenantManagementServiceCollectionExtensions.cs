using LinFx.Extensions.TenantManagement.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TenantManagementServiceCollectionExtensions
    {
        public static IServiceCollection AddLinFxTenantManagement(this IServiceCollection services)
        {
            services.AddTransient<ITenantService, TenantService>();
            return services;
        }
    }
}
