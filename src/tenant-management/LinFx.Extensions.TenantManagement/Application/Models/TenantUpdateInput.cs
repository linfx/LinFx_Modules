﻿using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.TenantManagement.Application.Models
{
    public class TenantUpdateInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }
    }
}