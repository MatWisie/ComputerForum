﻿using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumDbContext _context;
        public CommentRepository(ForumDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Comment> GetTopicComments(int topicId)
        {
            return _context.Comments.Where(e => e.TopicId == topicId);
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public Comment GetComment(int commentId)
        {
            return _context.Comments.FirstOrDefault(e => e.Id == commentId);
        }
        public void EditComment(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }
        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
