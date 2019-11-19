using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventCaveWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "First name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} must be at least {2} and at most {1} characters long.")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} must be at least {2} and at most {1} characters long.")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "{0} must be at least {2} and at most {1} characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9]+[_-]?[a-zA-Z0-9]+$", ErrorMessage = "The user name has to start and end with a letter or number. One '-' or '_' is allowed.")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}