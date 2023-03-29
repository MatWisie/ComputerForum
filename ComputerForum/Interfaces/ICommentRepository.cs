using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetTopicComments(int topicId);
        void AddComment(Comment comment);
        Comment GetComment(int commentId);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}