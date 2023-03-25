using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetTopicComments(int topicId);
    }
}