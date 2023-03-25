using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumDbContext _context;
        public TopicRepository(ForumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Topic> GetTopics(string categoryName)
        {
            return _context.Topics.Where(e => e.Category.Name == categoryName);
        }
        public Topic? GetTopic(int id)
        {
            return _context.Topics.FirstOrDefault(e => e.Id == id);
        }

    }
}
