using LinFx.Application.Models;
using LinFx.Extensions.Identity.Application.Models;
using LinFx.Extensions.Identity.Domain;
using LinFx.Extensions.Identity.EntityFrameworkCore;
using LinFx.Extensions.ObjectMapping;
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
                //.OrderBy(input.Sorting)
                .PageBy(1, 10)
                .ToListAsync();

            return new PagedResult<IdentityUserDto>(count, ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(items));
        }

        public async Task<IdentityUserDto> GetAsync(string id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _context.Users.FindAsync(id));
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
