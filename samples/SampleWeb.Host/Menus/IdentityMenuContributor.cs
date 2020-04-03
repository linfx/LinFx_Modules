using LinFx.Extensions.Identity.Permissions;
using LinFx.Extensions.UI.Navigation;
using System.Threading.Tasks;

namespace SampleWeb.Host.Menus
{
    public class IdentityMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "module1";
            context.Menu.DisplayName = "模块一";
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Roles.Default, "Menu:Role", "/Roles"));
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Users.Default, "Menu:User", "/Users"));
            return Task.CompletedTask;
        }
    }

    public class Identity2MenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "module2";
            context.Menu.DisplayName = "模块二";
            context.Menu.Items.Add(new ApplicationMenuItem("Role", "Menu:Role", "/Role"));
            context.Menu.Items.Add(new ApplicationMenuItem("User", "Menu:User", "/User"));
            return Task.CompletedTask;
        }
    }
}