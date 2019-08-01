using LinFx.Extensions.Identity.Application;
using LinFx.Extensions.UI.Navigation;
using System.Threading.Tasks;

namespace SampleWeb
{
    public class SampleWebMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.Name = "Identity";
            context.Menu.DisplayName = "导航一";
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.Role.Default, "Menu:Role", "/Roles"));
            context.Menu.Items.Add(new ApplicationMenuItem(IdentityPermissions.User.Default, "Menu:User", "/Users"));
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