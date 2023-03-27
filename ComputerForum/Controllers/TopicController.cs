using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            if(topicId == null || topicId == 0)
            {
                return NotFound();
            }
            var topic = _topicService.GetTopicWithComments(topicId);
            return View(topic);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(CommentCreateVM comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComment(comment);
                return RedirectToAction("Index", comment.TopicId);
            }
            return View(comment);
        }
        [Authorize]
        public IActionResult EditTopic(int topicId)
        {
            if (topicId == null || topicId == 0)
            {
                return NotFound();
            }

            var topic = _topicService.GetTopic(topicId);
            return View(topic);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTopic(TopicVM topic)
        {
            if (ModelState.IsValid)
            {
                _topicService.EditTopic(topic);
                return RedirectToAction("Index", "Home", "");
            }
            return View(topic);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int topicId)
        {
            var topic = _topicService.GetTopic(topicId);
            if(topic == null)
            {
                return NotFound();
            }
            _topicService.DeleteTopic(topic);
            return RedirectToAction("Index", "Home", "");
        }
    }
}
