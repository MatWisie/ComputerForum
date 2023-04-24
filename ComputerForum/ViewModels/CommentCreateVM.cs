using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CommentCreateVM
    {
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        [Required]
        public int TopicId { get; set; }
    }
}
