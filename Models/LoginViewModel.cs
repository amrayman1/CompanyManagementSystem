using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minimum password length is 5")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
