using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;
using System.Net;

namespace ComputerForum.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        public UserService(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public UserLoginVMResponse? LoginUser(UserLoginVM userVM)
        {
            var user = _userRepository.GetUser(userVM);
            if (user != null && BCrypt.Net.BCrypt.Verify(userVM.Password, user.Password))
            {
                if(user.Admin == true)
                {
                    UserLoginVMResponse tmpUserVM = new UserLoginVMResponse()
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Admin = true
                    };
                    return tmpUserVM;
                }
                else
                {
                    UserLoginVMResponse tmpUserVM = new UserLoginVMResponse()
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Admin = false
                    };
                    return tmpUserVM;
                }
            }
            return null;
        }

        public bool AddUser(UserRegisterVM userVM)
        {
            bool userExists = _userRepository.CheckIfUserExists(userVM);
            if (!userExists)
            {
                userVM.Password = BCrypt.Net.BCrypt.HashPassword(userVM.Password);
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
                _mailService.SendMail
                    (tmpUserVM.Email,
                    "Computer forum registration", 
                    $"Hello {tmpUserVM.Name}, we're glad you're here! \n " +
                    $"From now on, you can ask questions that bother you and help the community by answering other people's questions and participate in the life of our site \n" +
                    $"Thank you, Computer Forum team"
                    );

                return true;
            }
            else
            {
                return false;
            }

        }
        public int? GetUserId(string userName)
        {
            return _userRepository.GetUserIdByName(userName);
        }        
        public User? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public void AddReputation(int userId, int number)
        {
            var user = _userRepository.GetUserById(userId);
            user.Reputation += number;
            _userRepository.UpdateUser(user);
        }
        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }


    }
}
