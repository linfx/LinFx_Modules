using System.ComponentModel.DataAnnotations;

namespace LinFx.Identity.Domain.Models
{
    /// <summary>
    /// Represents a user in the identity system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 证件类型
        /// <Remark>
        /// 身份证 = 10,
        /// 普通护照 = 14,
        /// 出入境通行证 = 20,
        /// 暂住证 = 38,
        /// 永居证 = 50,
        /// 港澳台居住证 = 60,
        /// 其它 = 99,
        /// </Remark>
        /// </summary>
        public CertificateType CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [StringLength(200)]
        public string CertificateNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(200)]
        public string Avatar { get; set; }

        /// <summary>
        /// 性别(男1;女0)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日(格式: yyyy-MM-dd, 1998-05-05)
        /// </summary>
        [StringLength(20)]
        public string Birthday { get; set; }
    }
}
