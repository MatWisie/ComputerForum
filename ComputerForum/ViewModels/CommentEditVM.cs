using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CommentEditVM
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int CreatorId { get; set; }
    }
}
