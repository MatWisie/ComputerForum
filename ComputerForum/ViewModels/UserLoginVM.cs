using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
