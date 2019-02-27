using Identity.Domain.Models;
using LinFx;
using LinFx.Security.Authorization.Permissions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Application
{
    public class PermissionStore : IPermissionStore
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionStore(
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task<bool> IsGrantedAsync([NotNull] string name, [CanBeNull] string providerName, [CanBeNull] string providerKey)
        {
            var result = name == Permissions.User.Index &&
                         providerName == UserPermissionValueProvider.ProviderName;
            //&&providerKey == AuthTestController.FakeUserId.ToString();

            return Task.FromResult(result);
        }
    }
}