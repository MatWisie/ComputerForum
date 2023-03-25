using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        [ForeignKey("User")]
        public int CreatorId { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<ClickedReputation> ClickedReputations { get; set; }


    }
}
