using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class UserEditVM
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email does not meet the conditions")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
    }
}
