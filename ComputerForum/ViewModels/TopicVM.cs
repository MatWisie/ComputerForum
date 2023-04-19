using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class TopicVM
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please, add description")]
        public string Description { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
