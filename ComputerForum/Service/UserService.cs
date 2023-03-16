using ComputerForum.Interfaces;
using ComputerForum.ViewModels;

namespace ComputerForum.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserVM? GetUser(UserVM userVM)
        {
            var user = _userRepository.GetUser(userVM);
            if (user != null)
            {
                UserVM tmpUserVM = new UserVM()
                {
                    Active = user.Active,
                    Admin = user.Admin,
                    Age = user.Age,
                    Email = user.Email,
                    Gender = user.Gender,
                    Name = user.Name,
                    Password = user.Password,
                    Reputation = user.Reputation
                };
                return tmpUserVM;
            }
            return null;
        }
    }
}
