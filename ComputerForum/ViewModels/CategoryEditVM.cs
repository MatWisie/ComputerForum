using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CategoryEditVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public int CreatorId { get; set; }
    }
}
