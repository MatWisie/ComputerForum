using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        void AddToken(PasswordResetToken token);
        void DeleteToken(PasswordResetToken token);
        void DeleteUserTokens(int userId);
        PasswordResetToken? GetToken(string token);
    }
}