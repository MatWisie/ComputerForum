using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;


namespace ComputerForum.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _context;
        public UserRepository(ForumDbContext context)
        {
            _context = context;
        }
        public User? GetUser(UserLoginVM userVM)
        {
            return _context.Users.FirstOrDefault(e => e.Name == userVM.Name);
        }
        public bool CheckIfUserExists(UserRegisterVM userVM)
        {
            return _context.Users.Any(e => e.Name == userVM.Name);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
