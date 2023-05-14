using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class ReportCreateVM
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int ReportedUserId { get; set; }
        [Required]
        public int ReportCreatorId { get; set; }
        [Required]
        public int TopicId { get; set; }
    }
}
