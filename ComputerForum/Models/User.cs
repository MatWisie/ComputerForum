using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool Admin { get; set; }
        [Required]
        public int Reputation { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<Category> Categories { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Comment> Comments { get; set; }
        [InverseProperty("ReportedUser")]
        public ICollection<Report> ReportedUsers { get; set; }
        [InverseProperty("ReportCreator")]
        public ICollection<Report> ReportCreators { get; set; }
        public ICollection<ClickedReputation> ClickedReputations { get; set; }
        public ICollection<PasswordResetToken> PasswordResetTokens { get; set; }

    }
}
