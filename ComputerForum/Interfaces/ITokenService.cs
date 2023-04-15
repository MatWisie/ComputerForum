namespace ComputerForum.Interfaces
{
    public interface ITokenService
    {
        void GenerateForgotPasswordToken(string email);
    }
}