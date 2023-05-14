using ComputerForum.Models;
using ComputerForum.ViewModels;
using System.Net;

namespace ComputerForum.Interfaces
{
    public interface IUserService
    {
        UserLoginVMResponse? LoginUser(UserLoginVM userVM);
        void AddUser(UserRegisterVM userVM);
        int? GetUserId(string userName);
        User? GetUserById(int userId);
        User? GetUserByIdWithInclude(int userId);
        void AddReputation(int userId, int number);
        UserEditVM? GetUserToEditById(int userId);
        void UpdateUser(User user);
        void UpdateUser(UserEditVM user);
        void ChangePassword(User user);
        User? GetUserByName(string userName);
        User? GetUserByEmail(string userEmail);
    }
}