using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Application.Models
{
    public class IdentityRoleUpdateInput
    {
        [Required]
        public string Name { get; set; }
    }
}