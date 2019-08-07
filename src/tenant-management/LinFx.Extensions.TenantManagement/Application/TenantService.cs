using LinFx.Application.Models;
using LinFx.Extensions.ObjectMapping;
using LinFx.Extensions.TenantManagement.Application.Models;
using LinFx.Extensions.TenantManagement.Application.Modles;
using LinFx.Extensions.TenantManagement.Domain;
using LinFx.Extensions.TenantManagement.EntityFrameworkCore;
using LinFx.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinFx.Extensions.TenantManagement.Application
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public class TenantService : ITenantService
    {
        private readonly TenantManagementDbContext _context;

        public TenantService(TenantManagementDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TenantDto>> GetListAsync(TenantInput input)
        {
            var count = await _context.Tenants.CountAsync();
            var items = await _context.Tenants
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), u =>
                        u.Name.Contains(input.Filter))
                //.OrderBy(input.Sorting)
                .PageBy(1, 10)
                .ToListAsync();

            return new PagedResult<TenantDto>(count, ObjectMapper.Map<List<Tenant>, List<TenantDto>>(items));
        }

        public async Task<TenantDto> GetAsync(string id)
        {
            return ObjectMapper.Map<Tenant, TenantDto>(await _context.Tenants.FindAsync(id));
        }

        public async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            var item = ObjectMapper.Map<TenantCreateDto, Tenant>(input);
            item.Id = IDUtils.NewId().ToString();
            _context.Add(item);
            await _context.SaveChangesAsync(); 
            return ObjectMapper.Map<Tenant, TenantDto>(item);
        }

        public async Task<TenantDto> UpdateAsync(string id, TenantUpdateDto input)
        {
            var item = await _context.Tenants.FindAsync(id);
            if(item == null)
                return default;

            ObjectMapper.Map(input, item);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return ObjectMapper.Map<Tenant, TenantDto>(item);
        }

        public async Task DeleteAsync(string id)
        {
            var item = await _context.Tenants.FindAsync(id);
            _context.Tenants.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
