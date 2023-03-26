using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(UserLoginVM userVM);
        int? GetUserIdByName(string userName);
        public bool CheckIfUserExists(UserRegisterVM userVM);
    }
}