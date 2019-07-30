using LinFx.Application.Models;
using LinFx.Extensions.TenantManagement.Application;
using LinFx.Extensions.TenantManagement.Application.Models;
using LinFx.Extensions.TenantManagement.Application.Modles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinFx.Extensions.TenantManagement.HttpApi
{
    /// <summary>
    /// 租户Api
    /// </summary>
    [ApiController]
    [Route("api/multi-tenancy/[controller]")]
    public class TenantController : Controller
    {
        private readonly ITenantService _service;

        public TenantController(ITenantService service)
        {
            _service = service;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<PagedResult<TenantDto>> GetListAsync(TenantInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual Task<TenantDto> GetAsync(string id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual Task<TenantDto> UpdateAsync(string id, TenantUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(string id)
        {
            return _service.DeleteAsync(id);
        }
    }
}