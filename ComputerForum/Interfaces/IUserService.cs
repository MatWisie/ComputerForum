using ComputerForum.ViewModels;
using System.Net;

namespace ComputerForum.Interfaces
{
    public interface IUserService
    {
        UserLoginVM? LoginUser(UserLoginVM userVM);
        public bool AddUser(UserRegisterVM userVM);
        int? GetUserId(string userName);
    }
}