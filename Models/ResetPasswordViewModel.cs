using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minimum password length is 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
