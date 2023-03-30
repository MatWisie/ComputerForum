using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ICategoryService
    {
        IList<Category> GetCategories();
        Category? GetCategoryById(int categoryId);
        void DeleteCategory(Category category);
        void AddCategory(Category category);
        void EditCategory(Category category);
    }
}