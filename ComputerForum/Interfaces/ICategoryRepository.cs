using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void EditCategory(Category category);
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int categoryId);
        Category? GetCategoryByName(string categoryName);
    }
}