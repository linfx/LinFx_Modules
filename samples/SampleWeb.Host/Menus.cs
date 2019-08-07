using LinFx.Extensions.Identity.Application;
using LinFx.Extensions.UI.Navigation;
using System.Threading.Tasks;

namespace SampleWeb
{
    public class Menus : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "System";
            context.Menu.DisplayName = "系统管理";
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.User.Default, "用户管理", "/Users"));
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Role.Default, "角色管理", "/Roles"));
            return Task.CompletedTask;
        }
    }

    public class SampleWeb2MenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "Identiy2";
            context.Menu.DisplayName = "导航二";
            context.Menu.Items.Add(new ApplicationMenuItem("Role", "Menu:Role", "/Role"));
            context.Menu.Items.Add(new ApplicationMenuItem("User", "Menu:User", "/User"));
            return Task.CompletedTask;
        }
    }
}