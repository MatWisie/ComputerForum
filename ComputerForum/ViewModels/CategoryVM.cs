using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        public int CreatorId { get; set; }
    }
}
