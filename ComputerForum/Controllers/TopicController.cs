using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly ICommentService _commentService;
        public TopicController(ITopicService topicService, ICommentService commentService)
        {
            _topicService = topicService;
            _commentService = commentService;
        }

        public IActionResult Index(int topicId)
        {
            var topic = _topicService.GetTopic(topicId);
            return View(topic);
        }

        // POST: TopicController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(CommentCreateVM comment)
        {
            _commentService.AddComment(comment);
            return RedirectToAction("Index", comment.TopicId);
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
