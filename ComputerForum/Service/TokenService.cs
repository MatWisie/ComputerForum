using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Service
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        public TokenService(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
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
            }
        }
    }
}
