using System.ComponentModel.DataAnnotations;

namespace MobileWalletAdmin.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
        public string Password { get; set; }
    }


    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
