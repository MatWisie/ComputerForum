using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentCreateVM comment);
    }
}