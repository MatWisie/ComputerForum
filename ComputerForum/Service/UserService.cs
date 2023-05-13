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
                        Admin = true,
                        Active = user.Active
                    };
                    return tmpUserVM;
                }
                else
                {
                    UserLoginVMResponse tmpUserVM = new UserLoginVMResponse()
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Admin = false,
                        Active = user.Active
                    };
                    return tmpUserVM;
                }
            }
            return null;
        }

        public void AddUser(UserRegisterVM userVM)
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
        }
        public int? GetUserId(string userName)
        {
            return _userRepository.GetUserIdByName(userName);
        }        
        public User? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public User? GetUserByIdWithInclude(int userId)
        {
            return _userRepository.GetUserByIdWithInclude(userId);
        }

        public User? GetUserByName(string userName)
        {
            return _userRepository.GetUserByName(userName);
        }

        public User? GetUserByEmail(string userEmail)
        {
            return _userRepository.GetUserByEmail(userEmail);
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
        public void UpdateUser(UserEditVM user)
        {
            var userById = _userRepository.GetUserById(user.Id);
            userById.Name = user.Name;
            userById.Email = user.Email;
            userById.Age = user.Age;


            _userRepository.UpdateUser(userById);
        }
        public void ChangePassword(User user)
        {
            string tmpPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = tmpPassword;
            _userRepository.UpdateUser(user);
        }
        public UserEditVM? GetUserToEditById(int userId)
        {

            User? user = _userRepository.GetUserById(userId);
            UserEditVM tmpuser = new UserEditVM()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
            };
            return tmpuser;
        }

    }
}
