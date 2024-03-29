﻿using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerForum.Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumDbContext _context;
        public TopicRepository(ForumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Topic> GetTopics(int categoryId)
        {
            return _context.Topics.Where(e => e.Category.Id == categoryId).Include(e => e.User);
        }
        public Topic? GetTopic(int id)
        {
            return _context.Topics.FirstOrDefault(e => e.Id == id);
        }
        public Topic? GetTopicIncludeComments(int id)
        {
            return _context.Topics
                .Include(e => e.User)
                .Include(e => e.Comments)
                .ThenInclude(e => e.User)
                .FirstOrDefault(e => e.Id == id);
        }

        public void EditTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();
        }
        public void DeleteTopic(Topic topic)
        {
            _context.Topics.Remove(topic);
            _context.SaveChanges();
        }

        public void AddTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();
        }
        public int CountTopics()
        {
            return _context.Topics.Count();
        }

    }
}
