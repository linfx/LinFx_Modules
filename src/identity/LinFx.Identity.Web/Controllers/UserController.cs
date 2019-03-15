using System.Linq;
using System.Threading.Tasks;
using LinFx.Identity.Authorization;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.Web.Models.ManageViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static LinFx.Identity.Web.Models.ManageViewModels.ApplicationUserEditModel;

namespace Identity.Web.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: User
        public ActionResult Index()
        {
            var items = _userManager.Users;
            return View(items);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            //await _userManager.AddClaimAsync(user, new Claim(Permissions.User.Default, Permissions.User.Index));

            //var role = new ApplicationRole("user");
            //await _roleManager.CreateAsync(role);
            //role = await _roleManager.FindByNameAsync(role.Name);
            //await _roleManager.AddClaimAsync(role, new Claim(Permissions.User.Default, Permissions.User.Index));
            //await _userManager.AddToRoleAsync(user, role.Name);

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles;

            var model = new ApplicationUserEditModel
            {
                User = new ApplicationUserViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                }
            };

            var userRoleNames = await _userManager.GetRolesAsync(user);

            model.Roles = roles.Select(p => new AssignedRoleViewModel
            {
                Name = p.Name,
                IsAssigned = userRoleNames.Any(x => x == p.Name)
            }).ToArray();

            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUserEditModel input)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.UpdateAsync(new ApplicationUser
                {
                    PhoneNumber = input.User.PhoneNumber
                });

                foreach (var role in input.Roles)
                {
                    if (role.IsAssigned)
                        await _userManager.AddToRoleAsync(user, role.Name);
                    else
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}