using LinFx.Extensions.Identity.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddLinFxIdentity(this IServiceCollection services)
        {
            services.AddTransient<IdentityRoleService>();
            services.AddTransient<IdentityUserService>();
            return services;
        }
    }
}
