using LinFx.Utils;
using System;
using System.Threading.Tasks;

namespace LinFx.Extensions.TenantManagement.Domain
{
    public class TenantManager
    {
        //public async Task<Tenant> CreateAsync(string name)
        //{
        //    Check.NotNull(name, nameof(name));

        //    await ValidateNameAsync(name);
        //    return new Tenant(IDUtils.GenerateId().ToString(), name);
        //}

        //protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        //{
        //    var tenant = await _tenantRepository.FindByNameAsync(name);
        //    if (tenant != null && tenant.Id != expectedId)
        //    {
        //        throw new UserFriendlyException("Duplicate tenancy name: " + name); //TODO: A domain exception would be better..?
        //    }
        //}
    }
}
