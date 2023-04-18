using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ICategoryService
    {
        IList<Category> GetCategories();
        Category? GetCategoryById(int categoryId);
        void DeleteCategory(Category category);
        void AddCategory(CategoryVM category);
        void EditCategory(CategoryVM category);
        Category? GetCategoryByName(string categoryName);
    }
}