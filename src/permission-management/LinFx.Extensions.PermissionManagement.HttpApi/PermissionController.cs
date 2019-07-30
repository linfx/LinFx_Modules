using LinFx.Extensions.PermissionManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinFx.Extensions.PermissionManagement.HttpApi
{
    [ApiController]
    [Route("api/permission/[controller]")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionAppService)
        {
            _permissionService = permissionAppService;
        }

        [HttpGet]
        public Task<PermissionListResult> GetAsync(string providerName, string providerKey)
        {
            return _permissionService.GetAsync(providerName, providerKey);
        }

        [HttpPost]
        public Task UpdateAsync(string providerName, string providerKey, UpdatePermissionDto input)
        {
            return _permissionService.UpdateAsync(providerName, providerKey, input);
        }
    }
}
