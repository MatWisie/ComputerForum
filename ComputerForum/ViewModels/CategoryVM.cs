using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerForum.ViewModels
{
    public class CategoryVM
    {
        [Required]
        [Remote(action: "CheckUniqueName", controller: "CategoryValidation", ErrorMessage = "Category with that name already exists")]
        public string Name { get; set; }

        [Required]
        public int CreatorId { get; set; }
    }
}
