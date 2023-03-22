using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email does not meet the conditions")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least eight characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Choose your gender")]
        public string Gender { get; set; }
        public bool Active { get; set; } = true;
        public bool Admin { get; set; } = false;
        public int Reputation { get; set; } = 0;
    }
}
