using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly ForumDbContext _context;
        public PasswordResetTokenRepository(ForumDbContext context)
        {
            _context = context;
        }
        public void AddToken(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Add(token);
        }
        public void DeleteToken(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Remove(token);
        }
        public PasswordResetToken? GetToken(string token)
        {
            return _context.PasswordResetTokens.FirstOrDefault(e => e.Token == token);
        }
    }
}
