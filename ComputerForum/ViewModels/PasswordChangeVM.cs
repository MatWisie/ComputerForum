using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class PasswordChangeVM
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least eight characters long")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,100}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public int userId { get; set; }
    }
}
