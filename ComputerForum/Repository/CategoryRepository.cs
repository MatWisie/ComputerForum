using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

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
            return _context.Categories;
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
    }
}
