using LinFx.Application.Abstractions;
using LinFx.Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinFx.Identity.Application.Services
{
    [Authorize(IdentityPermissions.Users.Default)]
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<List<ApplicationUser>> GetListAsync()
        {
            return _userManager.Users.ToListAsync();
        }


    }
}
