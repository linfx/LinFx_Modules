using LinFx.Security.Authorization.Permissions;

namespace LinFx.Extensions.Identity.Application
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(IdentityPermissions.GroupName, L("Permission:IdentityManagement"));

            var rolesPermission = identityGroup.AddPermission(IdentityPermissions.Role.Default, L("Permission:RoleManagement"));
            rolesPermission.AddChild(IdentityPermissions.Role.Create, L("Permission:Create"));
            rolesPermission.AddChild(IdentityPermissions.Role.Edit, L("Permission:Edit"));
            rolesPermission.AddChild(IdentityPermissions.Role.Delete, L("Permission:Delete"));

            var usersPermission = identityGroup.AddPermission(IdentityPermissions.User.Default, L("Permission:UserManagement"));
            usersPermission.AddChild(IdentityPermissions.User.Create, L("Permission:Create"));
            usersPermission.AddChild(IdentityPermissions.User.Edit, L("Permission:Edit"));
            usersPermission.AddChild(IdentityPermissions.User.Delete, L("Permission:Delete"));
        }

        private static string L(string name)
        {
            return name;
        }
    }
}
