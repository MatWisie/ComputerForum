using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
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
        private readonly IRoleValidation _roleValidation;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, ITopicService topicService, IHttpContextAccessor httpContextAccessor, IRoleValidation roleValidation)
        {
            _logger = logger;
            _categoryService = categoryService;
            _topicService = topicService;
            _httpContextAccessor = httpContextAccessor;
            _roleValidation = roleValidation;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            ViewBag.metaDesc = "categories";
            ViewBag.metaKeywords = "categories";
            _logger.LogInformation("Rendered Index");
            return View(categories);
        }
        [Authorize]
        public IActionResult AddCategory()
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                _logger.LogError("Add categroy error, not authorized");
                return Unauthorized();
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddCategory(CategoryVM category)
        {
            category.CreatorId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            if (_roleValidation.CheckIfAdmin() != true)
            {
                _logger.LogError("Add categroy error, not authorized");
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                _logger.LogInformation("Category added");
                return RedirectToAction("Index");
            }
            _logger.LogError("AddCategory not valid model");
            return View(category);
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteCategory(int id) //this gonna be done with ajax
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                _logger.LogError("DeleteCategory error, category notfound");
                return NotFound();
            }
            if (_roleValidation.CheckIfAdmin() != true)
            {
                _logger.LogError("DeleteCategory error, unauthorized");
                return Unauthorized();
            }
            _logger.LogInformation("Category delted");
            _categoryService.DeleteCategory(category);
            return new JsonResult(Ok());
        }

        [Authorize]
        public IActionResult EditCategory(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                _logger.LogError("EditCategory error, not found");
                return NotFound();
            }
            if (_roleValidation.CheckIfAdmin() != true && Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != category.CreatorId)
            {
                _logger.LogError("EditCategory error, unauthorized");
                return Unauthorized();
            }

            CategoryEditVM tmp = new CategoryEditVM()
            {
                Id = category.Id,
                Name = category.Name,
                CreationDate = category.CreationDate,
                CreatorId = category.CreatorId,
            };

            return View(tmp); //here hide properties like Id etc. because we dont change those 
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCategory(CategoryEditVM category)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin" && Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != category.CreatorId)
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                Category tmp = new Category()
                {
                    Id = category.Id,
                    CreationDate = category.CreationDate,
                    CreatorId = category.CreatorId,
                    Name = category.Name
                };
                _categoryService.EditCategory(tmp);
                _logger.LogInformation("Category edited");
                return RedirectToAction("Index");
            }
            return View(category);
        }
        

        public IActionResult Topic(int id)
        {
            var topics = _topicService.GetTopics(id);
            ViewBag.metaDesc = "Topics";
            ViewBag.metaKeywords = "Topics";
            _logger.LogInformation("Entered Topic");
            return View(topics);
        }

        [Authorize]
        public IActionResult AddTopic(int id)
        {
            TopicVM tmp = new TopicVM();
            return View(tmp);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTopic(TopicVM topic)
        {
            topic.CreatorId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            if (ModelState.IsValid)
            {
                _topicService.AddTopic(topic);
                _logger.LogInformation("Created new Topic");
                return RedirectToAction("Topic", new {id = topic.CategoryId });
            }
            _logger.LogError("AddTopic error, model not valid");
            return View(topic);
            
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