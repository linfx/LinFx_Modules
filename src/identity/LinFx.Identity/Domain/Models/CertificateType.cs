using System.ComponentModel;

namespace LinFx.Identity.Domain.Models
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// 身份证 10
        /// </summary>
        [Description("身份证")]
        IdCard = 10,

        /// <summary>
        /// 普通护照 14
        /// </summary>
        [Description("普通护照")]
        Passport = 14,

        /// <summary>
        /// 中华人民共和国入出境通行证 20
        /// </summary>
        [Description("入出境通行证")]
        EntryExitPass = 20,

        /// <summary>
        /// 暂住证 38
        /// </summary>
        [Description("暂住证")]
        Temporary = 38,

        /// <summary>
        /// 永居证
        /// </summary>
        [Description("永居证")]
        Foreign = 50,

        /// <summary>
        /// 港澳台居住证
        /// </summary>
        [Description("港澳台居住证")]
        GATCard = 60,

        /// <summary>
        /// 其它证件 99
        /// </summary>
        [Description("其它")]
        Other = 99,
    }
}
