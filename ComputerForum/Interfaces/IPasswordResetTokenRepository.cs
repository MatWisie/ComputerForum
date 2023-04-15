using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        void AddToken(PasswordResetToken token);
        void DeleteToken(PasswordResetToken token);
        PasswordResetToken? GetToken(string token);
    }
}