using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.TenantManagement.Application.Models
{
    public class TenantCreateInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(200)]
        public virtual string Name { get; set; }
    }
}