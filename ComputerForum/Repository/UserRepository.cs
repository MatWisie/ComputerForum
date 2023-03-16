using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;


namespace ComputerForum.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IForumDbContext _context;
        public UserRepository(IForumDbContext context)
        {
            _context = context;
        }
        public User? GetUser(UserVM userVM)
        {
            return _context.Users.FirstOrDefault(e => e.Name == userVM.Name && e.Email == userVM.Email);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }
    }
}
