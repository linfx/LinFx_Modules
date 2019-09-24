using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Web.Models.ManageViewModels
{
    public class ApplicationRoleViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [HiddenInput]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Name { get; set; }
    }
}
