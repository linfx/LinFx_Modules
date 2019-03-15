using LinFx.UI.Navigation;
using System.Threading.Tasks;

namespace LinFx.Identity.Web.Host.Menus
{
    public class IdentityMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Items.Add(new ApplicationMenuItem("Home", "Menu:Home", "/"));
            context.Menu.Items.Add(new ApplicationMenuItem("Role", "Menu:Role", "/Role"));
            context.Menu.Items.Add(new ApplicationMenuItem("User", "Menu:User", "/User"));

            return Task.CompletedTask;
        }
    }

    public class Identity2MenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Items.Add(new ApplicationMenuItem("Home", "Menu:Home", "/"));
            context.Menu.Items.Add(new ApplicationMenuItem("Role", "Menu:Role", "/Role"));
            context.Menu.Items.Add(new ApplicationMenuItem("User", "Menu:User", "/User"));

            return Task.CompletedTask;
        }
    }
}
