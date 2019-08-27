using LinFx.Extensions.Identity.Application;
using Microsoft.AspNetCore.Identity;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static LinFxBuilder AddIdentity<TUser, TRole>(this LinFxBuilder fx, Action<IdentityOptions> options)
            where TUser : class
            where TRole : class
        {
            //services.AddTransient<IdentityRoleService>();
            fx.Services.AddTransient<IdentityUserService>();
            fx.Services.AddSingleton<IdentityPermissionDefinitionProvider>();

            fx.Services.AddIdentity<TUser, TRole>(options);

            return fx;
        }
    }
}
