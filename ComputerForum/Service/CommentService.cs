﻿using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
using System.Security.Claims;

namespace ComputerForum.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentService(IHttpContextAccessor httpContextAccessor, ICommentRepository commentRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _commentRepository = commentRepository;
        }

        public void AddComment(CommentCreateVM comment)
        {
            Comment tmp = new Comment()
            {
                Content = comment.Content,
                CreatorId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value),
                QuotedStatement = comment.QuotedStatement,
                TopicId = comment.TopicId,
            };
            _commentRepository.AddComment(tmp);
        }

        public Comment GetComment(int commentId)
        {
            return _commentRepository.GetComment(commentId);
        }

        public void EditComment(CommentVM comment)
        {
            Comment tmp = new Comment
            {
                Content = comment.Content,
                QuotedStatement = comment.QuotedStatement,
                CreatorId = comment.CreatorId,
                TopicId = comment.TopicId
            };
            _commentRepository.EditComment(tmp);
        }
        public void DeleteComment(Comment comment)
        {
            _commentRepository.DeleteComment(comment);
        }
    }
}