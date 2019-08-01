using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Web.Models.ManageViewModels
{
    public class ApplicationUserEditModel
    {
        public ApplicationUserViewModel User { get; set; }

        public AssignedRoleViewModel[] Roles { get; set; }

        public class ApplicationUserViewModel
        {
            [HiddenInput]
            public string Id { get; set; }

            [HiddenInput]
            public string ConcurrencyStamp { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "名称")]
            public string Name { get; set; }

            [StringLength(13)]
            [Display(Name = "手机")]
            public string PhoneNumber { get; set; }
        }

        public class AssignedRoleViewModel
        {
            [Required]
            [HiddenInput]
            public string Name { get; set; }

            public bool IsAssigned { get; set; }
        }
    }
}
