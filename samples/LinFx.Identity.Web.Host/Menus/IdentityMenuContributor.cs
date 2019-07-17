using LinFx.Identity.Application;
using LinFx.UI.Navigation;
using System.Threading.Tasks;

namespace LinFx.Identity.Web.Host.Menus
{
    public class IdentityMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "Identity";
            context.Menu.DisplayName = "导航一";
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Roles.Default, "Menu:Role", "/Roles"));
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Users.Default, "Menu:User", "/Users"));

            return Task.CompletedTask;
        }
    }

    public class Identity2MenuContributor : IMenuContributor
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