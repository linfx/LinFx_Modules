using System.ComponentModel.DataAnnotations;

namespace AuthServer.Host.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
