using ComputerForum.Interfaces;
using ComputerForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ComputerForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ITopicService _topicService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, ITopicService topicService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _categoryService = categoryService;
            _topicService = topicService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }
        [Authorize]
        public IActionResult AddCategory()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddCategory(Category category)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }

            _categoryService.DeleteCategory(category);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult EditCategory(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin" && Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != category.CreatorId)
            {
                return Unauthorized();
            }

            return View(category);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCategory(Category category)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin" && Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != category.CreatorId)
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                _categoryService.EditCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        

        public IActionResult Topic(int id)
        {
            var topics = _topicService.GetTopics(id);
            return View(topics);
        }

        [Authorize]
        public IActionResult AddTopic(int id)
        {
            return View(id);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTopic(Topic topic)
        {
            if (ModelState.IsValid)
            {
                _topicService.AddTopic(topic);
                return RedirectToAction("Topic", topic.CategoryId);
            }
            return View(topic);
            
        }

        public IActionResult TopicRedirect(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Topic", id);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}