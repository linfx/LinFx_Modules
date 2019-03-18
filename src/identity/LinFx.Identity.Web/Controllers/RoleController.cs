using System;
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
        public IActionResult Index()
        {
            var items = _roleManager.Roles;
            return View(items);
        }

        // GET: Role/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
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
        public async Task<IActionResult> Edit(string id)
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
        public async Task<IActionResult> Edit(string id, ApplicationRoleViewModel input)
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
        public IActionResult Delete(string id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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
        public async Task<IActionResult> Permissions(string id)
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
                Groups = groups.Select(p1 =>
                {
                    var claim1 = new Claim(p1.Name, p1.Name);
                    return new PermissionGroupViewModel
                    {
                        Name = p1.Name,
                        DisplayName = p1.DisplayName,
                        IsGranted = claims.Any(m => m.Type == claim1.Type && m.Value == claim1.Value),
                        Permissions = p1.Permissions.Select(p2 =>
                        {
                            var claim2 = new Claim(p1.Name, p2.Name);
                            return new PermissionGrantViewModel
                            {
                                Name = p2.Name,
                                DisplayName = p2.DisplayName,
                                IsGranted = claims.Any(m => m.Type == claim2.Type && m.Value == claim2.Value),
                                Children = p2.Children.Select(p3 =>
                                {
                                    var claim3 = new Claim(p1.Name, p3.Name);
                                    return new PermissionGrantViewModel
                                    {
                                        Name = p3.Name,
                                        DisplayName = p3.DisplayName,
                                        IsGranted = claims.Any(m => m.Type == claim3.Type && m.Value == claim3.Value),
                                    };
                                }).ToList()
                            };
                        }).ToList()
                    };
                }).ToList()
            };
            return View(model);
        }

        // POST: Role/Permissions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Permissions(string id, PermissionsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var p1 in model.Groups)
            {
                var claim1 = new Claim(p1.Name, p1.Name);
                await UpdatePermissionAsync(claim1, p1.IsGranted);

                foreach (var p2 in p1.Permissions)
                {
                    var claim2 = new Claim(p1.Name, p2.Name);
                    await UpdatePermissionAsync(claim2, p2.IsGranted);

                    foreach (var p3 in p2.Children)
                    {
                        var claim3 = new Claim(p1.Name, p3.Name);
                        await UpdatePermissionAsync(claim3, p3.IsGranted);
                    }
                }
            }

            async Task UpdatePermissionAsync(Claim claim, bool isGranted)
            {
                if (isGranted)
                {
                    if (!claims.Any(m => m.Type == claim.Type && m.Value == claim.Value))
                        await _roleManager.AddClaimAsync(role, claim);
                }
                else
                    await _roleManager.RemoveClaimAsync(role, claim);
            }

            return RedirectToAction("Index");
        }
    }
}