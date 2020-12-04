using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.Users;
using Backend.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.AspNetCore.Http;
using Backend.Service.Encryption;

namespace Backend.Service.SendingMail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EncryptionService _encryptionService;

        public MailService(IOptions<MailSettings> mailSettings, IHttpContextAccessor httpContextAccessor)
        {
            _mailSettings = mailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _encryptionService = new EncryptionService();
        }
        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            var MailText = ParseMailText(request, ReadMailText());
            var email = ConfigureEmail(request, MailText);
            await SendEmail(email);
        }

        private async Task SendEmail(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        private MimeMessage ConfigureEmail(WelcomeRequest request, string MailText)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.UserName}";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            return email;
        }

        private string ParseMailText(WelcomeRequest request, string MailText)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            string encryptedJmbg = _encryptionService.EncryptString(request.Jmbg);
            var parsedMailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail)
                .Replace("[jmbg]", encryptedJmbg).Replace("[host]", host);
            return parsedMailText;
        }

        private string ReadMailText()
        {
            var pathToHtmlPage = Path.Combine("Templates", "WelcomeTemplate.html");
            string FilePath = Directory.GetCurrentDirectory() + pathToHtmlPage;
            StreamReader reader = new StreamReader(FilePath);
            string MailText = reader.ReadToEnd();
            reader.Close();
            return MailText;
        }

    }
}
