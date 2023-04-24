using ComputerForum.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ComputerForum.Service
{
    public class MailService : IMailService
    {
        public void SendMail(string receiverEmail, string topic, string message)
        {
            try
            {
                var sender = new MailAddress("ourEmail@gmail.com", "Computer forum");
                var receiver = new MailAddress(receiverEmail);
                var password = System.Configuration.ConfigurationManager.AppSettings["EmailPass"];
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Address, password)
                };
                using (var mess = new MailMessage(sender, receiver)
                {
                    Subject = topic,
                    Body = message
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
