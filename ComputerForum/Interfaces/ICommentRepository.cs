using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetTopicComments(int topicId);
        void AddComment(Comment comment);
    }
}