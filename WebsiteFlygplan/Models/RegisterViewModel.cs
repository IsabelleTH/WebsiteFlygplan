using System.ComponentModel.DataAnnotations;

namespace WebsiteFlygplan.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Optional user name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Must match with the password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}