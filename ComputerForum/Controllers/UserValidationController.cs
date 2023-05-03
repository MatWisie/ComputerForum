using ComputerForum.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class UserValidationController : Controller
    {
        private readonly IUserService _userService;
        public UserValidationController(IUserService userService)
        {
            _userService = userService;
        }

        [AcceptVerbs("Get", "Post")]
        [HttpPost]
        public JsonResult CheckUniqueName(string Name)
        {
            if(_userService.GetUserByName(Name) == null)
            {
                return Json(true);
            }
            return Json(false);
        }
        [AcceptVerbs("Get", "Post")]
        [HttpPost]
        public JsonResult CheckUniqueEmail(string Email)
        {
            if(_userService.GetUserByEmail(Email) == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
