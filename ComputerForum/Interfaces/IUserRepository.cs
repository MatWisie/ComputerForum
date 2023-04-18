using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(UserLoginVM userVM);
        int? GetUserIdByName(string userName);
        User? GetUserById(int userId);
        User? GetUserByEmail(string email);
        User? GetUserByIdWithInclude(int userId);
        public bool CheckIfUserExists(UserRegisterVM userVM);
        void UpdateUser(User user);
    }
}