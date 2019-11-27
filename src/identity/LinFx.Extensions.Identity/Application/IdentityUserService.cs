using LinFx.Application.Models;
using LinFx.Extensions.Identity.Application.Models;
using LinFx.Extensions.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.Application
{
    public class IdentityUserService
    {
        private readonly IdentityDbContext _context;

        public IdentityUserService(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<IdentityUserDto>> GetListAsync(IdentityUserInput input)
        {
            var count = await _context.Users.CountAsync();
            var items = await _context.Users
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), u =>
                        u.UserName.Contains(input.Filter) ||
                        u.Email.Contains(input.Filter))
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResult<IdentityUserDto>(count, items.MapTo<IdentityUserDto[]>());
        }

        public async Task<IdentityUserDto> GetAsync(string id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null)
                throw new UserFriendlyException($"对象[{id}]不存在");

            return item.MapTo<IdentityUserDto>();
        }

        public Task<IdentityUserDto> UpdateAsync(string id, IdentityUserUpdateInput input)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDto> CreateAsync(IdentityUserInput input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
