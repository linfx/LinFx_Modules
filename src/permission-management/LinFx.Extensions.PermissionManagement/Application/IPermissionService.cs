using System.Threading.Tasks;
using LinFx.Extensions.PermissionManagement.Application.Models;

namespace LinFx.Extensions.PermissionManagement
{
    public interface IPermissionService
    {
        System.Threading.Tasks.Task<PermissionListResult> GetAsync(string providerName, string providerKey);
        Task UpdateAsync(string providerName, string providerKey, UpdatePermissionDto input);
    }
}