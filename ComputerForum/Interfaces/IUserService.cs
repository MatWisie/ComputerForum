using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IUserService
    {
        UserVM? GetUser(UserVM userVM);
    }
}