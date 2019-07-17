using EmitMapper;
using LinFx.Extensions.MultiTenancy;
using LinFx.Identity.Domain.Models;
using LinFx.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LinFx.Identity.Application.Services
{
    public class TenantStore : ITenantStore
    {
        private readonly ApplicationDbContext _context;

        public TenantStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TenantInfo> FindAsync(Guid parsedTenantId)
        {
            var tenant = await _context.Tenants.FindAsync(parsedTenantId);
            if (tenant == null)
                return null;

            return ObjectMapperManager.DefaultInstance.GetMapper<Tenant, TenantInfo>().Map(tenant);
        }

        public async Task<TenantInfo> FindAsync(string tenantIdOrName)
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(p => p.Name == tenantIdOrName);
            if (tenant == null)
                return null;

            return ObjectMapperManager.DefaultInstance.GetMapper<Tenant, TenantInfo>().Map(tenant);
        }
    }
}
