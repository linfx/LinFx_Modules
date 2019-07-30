using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Application.Models
{
    public class IdentityRoleUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}