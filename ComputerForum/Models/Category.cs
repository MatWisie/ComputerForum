using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("User")]
        public int CreatorId { get; set; }

        public User User { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
