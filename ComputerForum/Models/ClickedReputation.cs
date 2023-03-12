using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class ClickedReputation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsGoodOpinion { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
