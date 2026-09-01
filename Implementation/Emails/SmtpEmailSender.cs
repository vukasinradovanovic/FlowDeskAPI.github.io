using Application.Flowdesk.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Implementation.Emails
{
    //https://myaccount.google.com/apppasswords - postavljanje app password-a za gmail
    public class SmtpEmailSender : IEmailSender
    {
        private string _fromEmail;
        private string _appPassword;
        private string _smtpHost;
        private int _smtpPort;
        private string _username;

        public SmtpEmailSender(string fromEmail, string appPassword, string smtpHost, int smtpPort, string username)
        {
            _fromEmail = fromEmail;
            _appPassword = appPassword;
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _username = username;
        }

        public void SendEmail(string recipient, string subject, string htmlContent)
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_fromEmail, "FlowDesk"),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };

            message.To.Add(recipient);

            using SmtpClient client = new SmtpClient(_smtpHost, _smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _appPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            client.Send(message);
        }
    }
}
