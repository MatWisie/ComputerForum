using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentCreateVM comment);
        Comment? GetComment(int commentId);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
        void EditCommentVM(CommentEditVM comment);

    }
}