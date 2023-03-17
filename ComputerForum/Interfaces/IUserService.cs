using ComputerForum.ViewModels;
using System.Net;

namespace ComputerForum.Interfaces
{
    public interface IUserService
    {
        UserLoginVM? GetUser(UserLoginVM userVM);
        public bool AddUser(UserRegisterVM userVM);
    }
}