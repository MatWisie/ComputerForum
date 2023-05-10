using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComputerForum.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ForumDbContext _context;
        public CategoryRepository(ForumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.Include(e => e.User);
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public Category? GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(e => e.Id == categoryId);
        }
        public Category? GetCategoryByName(string categoryName)
        {
            return _context.Categories.FirstOrDefault(e => e.Name == categoryName);
        }
        public void EditCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        public int CountCategories()
        {
            return _context.Categories.Count();
        }
    }
}
