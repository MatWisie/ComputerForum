using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
    }
}
