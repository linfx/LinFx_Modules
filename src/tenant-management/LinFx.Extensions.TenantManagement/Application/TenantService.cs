using LinFx.Application.Models;
using LinFx.Extensions.ObjectMapping;
using LinFx.Extensions.TenantManagement.Application.Models;
using LinFx.Extensions.TenantManagement.Application.Modles;
using LinFx.Extensions.TenantManagement.Domain;
using LinFx.Extensions.TenantManagement.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinFx.Extensions.TenantManagement.Application
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public class TenantService : ITenantService
    {
        private readonly TenantManagementDbContext _context;

        //public TenantService(TenantManagementDbContext context)
        //{
        //    _context = context;
        //}

        public Task<PagedResult<TenantDto>> GetListAsync(TenantInput input)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TenantDto> GetAsync(string id)
        {
            return ObjectMapper.Map<Tenant, TenantDto>(await _context.Tenants.FindAsync(id));
        }

        public Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            throw new System.NotImplementedException();
        }

        public Task<TenantDto> UpdateAsync(string id, TenantUpdateDto input)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
