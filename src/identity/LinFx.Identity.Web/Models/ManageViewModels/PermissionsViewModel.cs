using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Identity.Web.Models.ManageViewModels
{
    public class PermissionsViewModel
    {
        public List<PermissionGroupModel> PermissionGroups { get; set; }
    }

    public class PermissionGroupModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<PermissionGrantModel> Permissions { get; set; } = new List<PermissionGrantModel>();
    }

    public class PermissionGrantModel
    {
        [Required]
        [HiddenInput]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsGranted { get; set; }
    }
}
