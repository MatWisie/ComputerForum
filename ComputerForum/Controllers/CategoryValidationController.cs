using ComputerForum.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class CategoryValidationController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryValidationController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        [HttpPost]
        public JsonResult CheckUniqueName(string Name)
        {
            if (_categoryService.GetCategoryByName(Name) == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
