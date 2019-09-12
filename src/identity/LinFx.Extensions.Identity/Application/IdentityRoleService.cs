using LinFx.Application.Models;
using LinFx.Extensions.Identity.Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.Application
{
    public class IdentityRoleService<TRole> where TRole : class
    {
        private readonly RoleManager<TRole> _roleManager;

        public IdentityRoleService(RoleManager<TRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return;

            await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityRoleDto> GetAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return default;

            return role.MapTo<IdentityRoleDto>();
        }

        public Task<IdentityRoleDto> UpdateAsync(string id, IdentityRoleUpdateInput input)
        {
            var item = new IdentityRoleDto();
            return Task.FromResult(item);
        }

        public Task<IdentityUserDto> CreateAsync(IdentityUserInput input)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<IdentityRoleDto>> GetList(IdentityRoleInput input)
        {
            throw new NotImplementedException();
        }
    }
}
