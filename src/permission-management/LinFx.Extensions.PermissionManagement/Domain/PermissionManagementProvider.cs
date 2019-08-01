using LinFx.Extensions.MultiTenancy;
using LinFx.Security.Authorization.Permissions;
using System;
using System.Threading.Tasks;

namespace LinFx.Extensions.PermissionManagement
{
    public abstract class PermissionManagementProvider : IPermissionManagementProvider
    {
        public abstract string Name { get; }

        protected IPermissionGrantRepository PermissionGrantRepository { get; }

        protected ICurrentTenant CurrentTenant { get; }

        public virtual async Task<PermissionValueProviderGrantInfo> CheckAsync(string name, string providerName, string providerKey)
        {
            if (providerName != Name)
            {
                return PermissionValueProviderGrantInfo.NonGranted;
            }

            return new PermissionValueProviderGrantInfo(
                await PermissionGrantRepository.FindAsync(name, providerName, providerKey) != null,
                providerKey
            );
        }

        public virtual Task SetAsync(string name, string providerKey, bool isGranted)
        {
            return isGranted
                ? GrantAsync(name, providerKey)
                : RevokeAsync(name, providerKey);
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        protected virtual async Task GrantAsync(string name, string providerKey)
        {
            var permissionGrant = await PermissionGrantRepository.FindAsync(name, Name, providerKey);
            if (permissionGrant != null)
            {
                return;
            }

            await PermissionGrantRepository.InsertAsync(
                new PermissionGrant(
                    Guid.NewGuid(),
                    name,
                    Name,
                    providerKey,
                    CurrentTenant.Id
                )
            );
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        protected virtual async Task RevokeAsync(string name, string providerKey)
        {
            var permissionGrant = await PermissionGrantRepository.FindAsync(name, Name, providerKey);
            if (permissionGrant == null)
            {
                return;
            }
            await PermissionGrantRepository.DeleteAsync(permissionGrant);
        }
    }
}
