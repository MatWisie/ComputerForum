using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ITokenService
    {
        void GenerateForgotPasswordToken(string email);
        PasswordResetToken? GetForgotPasswordToken(string token);
        void DeleteForgotPasswordToken(string token);
        void DeleteUserForgotPasswordTokens(int userId);
    }
}