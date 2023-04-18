using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;

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
        public Category? GetCategoryByName(string categoryName)
        {
            return _categoryRepository.GetCategoryByName(categoryName);
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.DeleteCategory(category);
        }

        public void AddCategory(CategoryVM category)
        {
            Category tmp = new Category
            {
                Name = category.Name,
                CreatorId = category.CreatorId
            };
            _categoryRepository.AddCategory(tmp);
        }
        public void EditCategory(Category category)
        {
            _categoryRepository.EditCategory(category);
        }
    }
}
