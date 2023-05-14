﻿using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly ICommentService _commentService;
        private readonly IReputationService _reputationService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleValidation _roleValidation;
        public TopicController(ITopicService topicService, ICommentService commentService, IHttpContextAccessor httpContextAccessor, IUserService userService, IReputationService reputationService, IReportService reportService, IRoleValidation roleValidation)
        {
            _topicService = topicService;
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _reputationService = reputationService;
            _reportService = reportService;
            _roleValidation = roleValidation;
        }

        public IActionResult Index(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var topic = _topicService.GetTopicWithComments(id);
            return View(topic);
        }

        [Authorize]
        public IActionResult CreateComment(int id, int? quotedId)
        {
            CommentCreateVM tmp = new CommentCreateVM();
            tmp.TopicId = id;
            if(quotedId != null)
            {
                var quotedComment = _commentService.GetComment((int)quotedId);
                tmp.QuotedStatement = quotedComment.Content;
            }
            return PartialView(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(CommentCreateVM comment)
        {
            comment.CreatorId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            var topic = _topicService.GetTopic(comment.TopicId);
            if (ModelState.IsValid && topic.Active)
            {
                _commentService.AddComment(comment);
                return RedirectToAction("Index", new { id = comment.TopicId });
            }
            return PartialView(comment);
        }

        [Authorize]
        public IActionResult EditTopic(int topicId)
        {
            if (topicId == null || topicId == 0)
            {
                return NotFound();
            }

            var topic = _topicService.GetTopic(topicId);
            if(topic == null)
            {
                return NotFound();
            }

            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != topic.CreatorId)
            {
                return Unauthorized();
            }

            TopicEditVM tmp = new TopicEditVM()
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                CreatorId = topic.CreatorId,
                CategoryId = topic.CategoryId
            };

            return View(tmp);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditTopic(TopicEditVM topic)
        {
            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != topic.CreatorId)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _topicService.EditTopic(topic);
                return RedirectToAction("Index", new {id = topic.Id});
            }
            return View(topic);
        }

        [Authorize]
        public IActionResult EditComment(int commentId)
        {

            if (commentId == null || commentId == 0)
            {
                return NotFound();
            }
            var comment = _commentService.GetComment(commentId);
            if(comment == null || comment.Content == "Comment was deleted")
            {
                return NotFound();
            }
            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != comment.CreatorId)
            {
                return Unauthorized();
            }

            CommentEditVM tmp = new CommentEditVM()
            {
                Id = comment.Id,
                Content = comment.Content,
                QuotedStatement = comment.QuotedStatement,
                CreatorId = comment.CreatorId,
                TopicId = comment.TopicId
            };

            return View(tmp);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditComment(CommentEditVM comment)
        {
            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != comment.CreatorId)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _commentService.EditCommentVM(comment);
                return RedirectToAction("Index", new {id = comment.TopicId});
            }
            return View(comment);
        }

        [Authorize]
        public IActionResult DeleteComment(int commentId)
        {
            var comment = _commentService.GetComment(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            if (_roleValidation.CheckIfAdmin() != true || Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != comment.CreatorId)
            {
                return Unauthorized();
            }
            comment.Content = "Comment was deleted";
            _commentService.EditComment(comment);
            return RedirectToAction("Index", new {id = comment.TopicId});
        }

        [Authorize]
        public IActionResult DeleteTopic(int topicId)
        {
            var topic = _topicService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound();
            }
            if (_roleValidation.CheckIfAdmin() != true || Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != topic.CreatorId)
            {
                return Unauthorized();
            }

            _topicService.DeleteTopic(topic);
            return RedirectToAction("Topic", "Home", new {id = topic.CategoryId});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReputationButton(int topicId, bool isPositive)
        {
            var topic = _topicService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound();
            }

            string result = _reputationService.AddToClickedReputations(Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value), topicId, isPositive);
            User user = _userService.GetUserById(Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            if(result == "Added")
            {
                return new JsonResult(user.Reputation);
            }
            if (result == "Deleted")
            {
                return new JsonResult(user.Reputation);
            }
            else
            {
                return new JsonResult("Something went wrong");
            }
        }

        [Authorize]
        public IActionResult ReportTopic(int topicId)
        {
            Topic? topic = _topicService.GetTopic(topicId);
            if(topic != null && topic.CreatorId != Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value))
            {
                ReportCreateVM tmpreport = new ReportCreateVM()
                {
                    ReportedUserId = topic.CreatorId,
                    TopicId = topicId
                };
                return View(tmpreport);
            }
            return NotFound();
            
        }
        [Authorize]
        [HttpPost]
        public IActionResult ReportTopic(ReportCreateVM report)
        {
            report.ReportCreatorId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            if (ModelState.IsValid)
            {
                _reportService.AddReport(report);
                return RedirectToAction("Index", "Topic", new { id = report.TopicId });
            }
            return View(report);
        }

        [Authorize]
        public IActionResult CloseTopic(int topicId)
        {
            var topic = _topicService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound();
            }
            if (_roleValidation.CheckIfAdmin() != true || Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) != topic.CreatorId)
            {
                return Unauthorized();
            }

            _topicService.CloseTopic(topic);
            return RedirectToAction("Index", new { id = topic.Id });
        }
    }
}
