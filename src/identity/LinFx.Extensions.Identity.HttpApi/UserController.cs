using LinFx.Application.Models;
using LinFx.Extensions.Identity.Application;
using LinFx.Extensions.Identity.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.HttpApi
{
    /// <summary>
    /// 用户Api
    /// </summary>
    [ApiController]
    [Route("api/identity/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IdentityUserService _userService;

        public UserController(IdentityUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<PagedResult<IdentityUserDto>> GetListAsync(IdentityUserInput input)
        {
            return _userService.GetListAsync(input);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual Task<IdentityUserDto> GetAsync(string id)
        {
            return _userService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual Task<IdentityUserDto> CreateAsync(IdentityUserInput input)
        {
            return _userService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual Task<IdentityUserDto> UpdateAsync(string id, IdentityUserUpdateInput input)
        {
            return _userService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(string id)
        {
            return _userService.DeleteAsync(id);
        }
    }
}
