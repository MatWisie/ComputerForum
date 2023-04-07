namespace ComputerForum.Interfaces
{
    public interface IMailService
    {
        void SendMail(string receiverEmail, string topic, string message);
    }
}