using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
using System.Net;

namespace ComputerForum.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserLoginVM? GetUser(UserLoginVM userVM)
        {
            var user = _userRepository.GetUser(userVM);
            if (user != null)
            {
                UserLoginVM tmpUserVM = new UserLoginVM()
                {
                    Name = user.Name,
                    Password = user.Password
                };
                return tmpUserVM;
            }
            return null;
        }

        public bool AddUser(UserRegisterVM userVM)
        {
            bool userExists = _userRepository.CheckIfUserExists(userVM);
            if(!userExists)
            {
                User tmpUserVM = new User()
                {
                    Name = userVM.Name,
                    Password = userVM.Password,
                    Email = userVM.Email,
                    Age = userVM.Age,
                    Gender = userVM.Gender,
                    Active = userVM.Active,
                    Admin = userVM.Admin,
                    Reputation = userVM.Reputation,
                };
                _userRepository.AddUser(tmpUserVM);

                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
