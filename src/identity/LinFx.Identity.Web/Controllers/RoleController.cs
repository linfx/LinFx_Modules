using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LinFx.Identity.Authorization;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.Web.Models.ManageViewModels;
using LinFx.Security.Authorization.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    //[Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public RoleController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IPermissionDefinitionManager permissionDefinitionManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        // GET: Role
        public ActionResult Index()
        {
            var items = _roleManager.Roles;
            return View(items);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
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

        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var model = new ApplicationRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp,
            };
            return View(model);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, ApplicationRoleViewModel input)
        {
            if (ModelState.IsValid)
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(id);
                if (await TryUpdateModelAsync(roleToUpdate))
                {
                    await _roleManager.SetRoleNameAsync(roleToUpdate, input.Name);
                    var result = await _roleManager.UpdateAsync(roleToUpdate);
                    if (result.Succeeded)
                    {
                        //return LocalRedirect(returnUrl);
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Role/Delete/5
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

        // GET: Role/Permissions/5
        public IActionResult Permissions(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<ActionResult> GetPermissions(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var claims = await _roleManager.GetClaimsAsync(role);

            var groups = _permissionDefinitionManager.GetGroups();


            var model = new PermissionsViewModel
            {
                PermissionGroups = new List<PermissionGroupModel>()
            };

            foreach (var sys in groups)
            {
                foreach (var mod in sys.Permissions)
                {
                    var group = new PermissionGroupModel
                    {
                        Name = mod.Name,
                        DisplayName = mod.DisplayName
                    };

                    foreach (var item in mod.Children)
                    {
                        var claim = new Claim(group.Name, item.Name);

                        group.Permissions.Add(new PermissionGrantModel
                        {
                            Name = item.Name,
                            DisplayName = item.DisplayName,
                            IsGranted = claims.Any(m => m.Type == claim.Type && m.Value == claim.Value)
                        });
                    }

                    model.PermissionGroups.Add(group);
                }
            }
            return Json(model);
        }

        // POST: Role/Permissions/5
        [HttpPost]
        public async Task<IActionResult> Permissions(string id, [FromBody] PermissionsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var group in model.PermissionGroups)
            {
                foreach (var item in group.Permissions)
                {
                    var claim = new Claim(group.Name, item.Name);

                    if (item.IsGranted)
                    {
                       if(!claims.Any(m => m.Type == claim.Type && m.Value == claim.Value))
                            await _roleManager.AddClaimAsync(role, claim);
                    }
                    else
                        await _roleManager.RemoveClaimAsync(role, claim);
                }
            }

            return Json("success");
        }
    }
}