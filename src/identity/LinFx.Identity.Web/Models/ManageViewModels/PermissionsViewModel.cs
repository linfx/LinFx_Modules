using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Identity.Web.Models.ManageViewModels
{
    public class PermissionsViewModel
    {
        /// <summary>
        /// PermissionGroups
        /// </summary>
        public List<PermissionGroupViewModel> Groups { get; set; }
    }

    public class PermissionGroupViewModel
    {
        [Required]
        [HiddenInput]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsGranted { get; set; }

        public List<PermissionGrantViewModel> Permissions { get; set; }
    }

    public class PermissionGrantViewModel
    {
        [Required]
        [HiddenInput]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsGranted { get; set; }

        public List<PermissionGrantViewModel> Children { get; set; }
    }
}
