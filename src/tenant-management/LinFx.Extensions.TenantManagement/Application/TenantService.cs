using LinFx.Application;
using LinFx.Extensions.TenantManagement.Application.Models;
using LinFx.Extensions.TenantManagement.Domain;
using LinFx.Extensions.TenantManagement.EntityFrameworkCore;

namespace LinFx.Extensions.TenantManagement.Application
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [Service]
    public class TenantService : CrudService<Tenant, TenantDto, TenantDto, string, TenantInput, TenantCreateInput, TenantUpdateInput>, ITenantService
    {
        public TenantService(ServiceContext context, TenantManagementDbContext db)
            : base(context, db)
        {
        }

        protected TenantManagementDbContext Db
        {
            get { return (TenantManagementDbContext)_db; }
        }
    }
}
