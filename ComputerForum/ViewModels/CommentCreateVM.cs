using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CommentCreateVM
    {
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(1000, ErrorMessage = "Max length is 1000 characters")]
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int CreatorId { get; set; }
    }
}
