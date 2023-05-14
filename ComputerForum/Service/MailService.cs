using ComputerForum.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ComputerForum.Service
{
    public class MailService : IMailService
    {
        public void SendMail(string receiverEmail, string topic, string messagestring)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(receiverEmail);
            mail.From = new MailAddress("shzstrl2@mailosaur.net", "ROBOT", System.Text.Encoding.UTF8);
            mail.Subject = topic;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestring;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("shzstrl2@mailosaur.net", "password"); //here will be needed new password, because i made test account
            client.Port = 587;
            client.Host = "smtp.mailosaur.net";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}

//xsmtpsib-4f290665cdffdfe6531986a340f28ce2cf9cbc1c6bd09e4dbcba79d3ba51d736-SNC1pcPkHhgjQrW7
