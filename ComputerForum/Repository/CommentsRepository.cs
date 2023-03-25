using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ForumDbContext _context;
        public CommentsRepository(ForumDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Comment> GetTopicComments(int topicId)
        {
            return _context.Comments.Where(e => e.TopicId == topicId);
        }
    }
}
