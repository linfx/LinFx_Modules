using LinFx.Application.Models;
using LinFx.Extensions.Identity.Application.Models;
using System;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.Application
{
    public class IdentityRoleService
    {
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRoleDto> GetAsync(string id)
        {
            var item = new IdentityRoleDto();
            return Task.FromResult(item);
        }

        public Task<IdentityRoleDto> UpdateAsync(string id, IdentityRoleUpdateDto input)
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
