using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [MinLength(5, ErrorMessage ="Minimum password length is 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password), ErrorMessage ="Password mismatch")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
