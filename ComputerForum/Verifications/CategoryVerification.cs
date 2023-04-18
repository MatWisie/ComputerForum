using ComputerForum.Interfaces;

namespace ComputerForum.Verifications
{
    public class CategoryVerification : ICategoryVerification
    {
        private readonly ICategoryService _categoryService;
        public CategoryVerification(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public bool CheckUniqueName(string categoryName)
        {
            if (_categoryService.GetCategoryByName(categoryName) == null)
            {
                return true;
            }
            return false;
        }
    }
}
