using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        [Required]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int CreatorId { get; set; }

        public Topic Topic { get; set; }
        public User User { get; set; }
    }
}
