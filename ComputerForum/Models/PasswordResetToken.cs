using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerForum.Models
{
    public class PasswordResetToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; } = DateTime.Today.AddDays(1);
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
