using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IList<Category> GetCategories()
        {
            return _categoryRepository.GetCategories().ToList();
        }
        public Category? GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.DeleteCategory(category);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }
        public void EditCategory(Category category)
        {
            _categoryRepository.EditCategory(category);
        }
    }
}
