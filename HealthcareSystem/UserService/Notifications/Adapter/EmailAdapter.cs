using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using UserService.CustomException;

namespace UserService.Notifications
{
    class EmailAdapter : INotificationSender
    {
        private readonly MailSettings _mailSettings;

        public EmailAdapter(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendNotification(string notification, string recipientEmail)
        {
            try
            {
                MimeMessage message = ConfigureMessage(notification, recipientEmail);
                SendEmail(message);
            }
            catch (Exception)
            {
                throw new ExternalConnectionException();
            }
        }

        private MimeMessage ConfigureMessage(string notification, string recipientEmail)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = $"Welcome!";

            var builder = new BodyBuilder();
            builder.HtmlBody = notification;
            email.Body = builder.ToMessageBody();
            return email;
        }

        private void SendEmail(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
