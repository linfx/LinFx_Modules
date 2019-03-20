using LinFx.Security.Authorization.Permissions;

namespace LinFx.Identity.Application
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(IdentityPermissions.GroupName);

            //用户
            var user = group.AddPermission(IdentityPermissions.Users.Default, "Permission:User");
            user.AddChild(IdentityPermissions.Users.Index, "Permission:Index");
            user.AddChild(IdentityPermissions.Users.Edit, "Permission:Edit");
            user.AddChild(IdentityPermissions.Users.Delete, "Permission:Delete");
            user.AddChild(IdentityPermissions.Users.Create, "Permission:Create");
            user.AddChild(IdentityPermissions.Users.Details, "Permission:Details");

            //角色
            var role = group.AddPermission(IdentityPermissions.Roles.Default, "Permission:Role");
            role.AddChild(IdentityPermissions.Roles.Index, "Permission:Index");
            role.AddChild(IdentityPermissions.Roles.Edit, "Permission:Edit");
            role.AddChild(IdentityPermissions.Roles.Delete, "Permission:Delete");
            role.AddChild(IdentityPermissions.Roles.Create, "Permission:Create");
            role.AddChild(IdentityPermissions.Roles.Details, "Permission:Details");
            role.AddChild(IdentityPermissions.Roles.Permissions, "Permission:Permissions");
        }
    }
}
