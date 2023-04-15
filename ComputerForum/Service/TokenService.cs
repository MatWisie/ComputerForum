using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Service
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IMailService _mailService;
        public TokenService(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _mailService = mailService;
        }

        public void GenerateForgotPasswordToken(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user != null)
            {
                PasswordResetToken token = new PasswordResetToken();
                do
                {
                    token.Token = Guid.NewGuid().ToString();
                    token.UserId = user.Id;
                } while (_passwordResetTokenRepository.GetToken(token.Token) != null);

                _passwordResetTokenRepository.AddToken(token);
                _mailService.SendMail
                    (user.Email, 
                    "Computer Forum password reset", 
                    $"Hello, \n we heard that you want to change your account password, " +
                    $"no worries, here is link: https://localhost:7176/User/NewUserPassword?token={token.Token} \n" + //Here link may differ if server changes
                    $"If it wasn't you, please ignore this message"); 
            }
        }
        public PasswordResetToken? GetForgotPasswordToken(string token)
        {
            return _passwordResetTokenRepository.GetToken(token);
        }
        public void DeleteForgotPasswordToken(string token)
        {
            var tmp = _passwordResetTokenRepository.GetToken(token);
            _passwordResetTokenRepository.DeleteToken(tmp);
        }
        public void DeleteUserForgotPasswordTokens(int userId)
        {
            _passwordResetTokenRepository.DeleteUserTokens(userId);
        }
    }
}
