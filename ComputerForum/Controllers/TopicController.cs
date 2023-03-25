using ComputerForum.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        // GET: TopicController
        public IActionResult Index(string categoryName)
        {
            var topics = _topicService.GetTopics(categoryName);
            return View(topics);
        }

        // GET: TopicController/Details/5
        public IActionResult Details(int id)
        {
            var topic = _topicService.GetTopic(id);
            return View(topic);
        }

        // GET: TopicController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopicController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TopicController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: TopicController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TopicController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: TopicController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
