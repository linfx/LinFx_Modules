using System.Threading.Tasks;
using LinFx.Application.Models;
using LinFx.Extensions.TenantManagement.Application.Models;
using LinFx.Extensions.TenantManagement.Application.Modles;

namespace LinFx.Extensions.TenantManagement.Application
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public interface ITenantService
    {
        Task<TenantDto> GetAsync(string id);
        Task<PagedResult<TenantDto>> GetListAsync(TenantInput input);
        Task<TenantDto> CreateAsync(TenantCreateDto input);
        Task<TenantDto> UpdateAsync(string id, TenantUpdateDto input);
        Task DeleteAsync(string id);
    }
}
