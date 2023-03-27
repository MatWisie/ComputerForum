using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentCreateVM comment);
        Comment GetComment(int commentId);
        void EditComment(CommentVM comment);
        void DeleteComment(Comment comment);
    }
}