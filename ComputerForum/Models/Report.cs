using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ReportedUserId { get; set; }
        [Required]
        public int ReportCreatorId { get; set; }
        [Required]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        [ForeignKey("ReportedUserId")]
        public User ReportedUser { get; set; }

        [ForeignKey("ReportCreatorId")]
        public User ReportCreator { get; set; }
        public Topic Topic { get; set; }
    }
}
