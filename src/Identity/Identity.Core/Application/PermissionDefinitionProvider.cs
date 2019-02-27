using LinFx.Security.Authorization.Permissions;
using Microsoft.Extensions.Localization;

namespace Identity.Application
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(Permissions.GroupName);

            //Define your own permissions here.
            var user = group.AddPermission(Permissions.User.Default, L("Permission:User"));
            user.AddChild(Permissions.User.Index, L("Permission:Index"));
            user.AddChild(Permissions.User.Edit, L("Permission:Edit"));
            user.AddChild(Permissions.User.Delete, L("Permission:Delete"));
            user.AddChild(Permissions.User.Create, L("Permission:Create"));

            var role = group.AddPermission(Permissions.Role.Default, L("Permission:Role"));
            role.AddChild(Permissions.Role.Index, L("Permission:Index"));
            role.AddChild(Permissions.Role.Edit, L("Permission:Edit"));
            role.AddChild(Permissions.Role.Delete, L("Permission:Delete"));
            role.AddChild(Permissions.Role.Create, L("Permission:Create"));
        }

        private static IStringLocalizer L(string name)
        {
            return null;
        }
    }
}
