using Eticaret.SharedLibrary.Email.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Eticaret.SharedLibrary.Email
{
    public class MailTrapEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public MailTrapEmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void Send(string toMail, string subject, string body)
        {
            // SmtpClient: Sunucu ayarları
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };

            // MailMessage: Mail ayarları
            var mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.FromMail, _emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress(toMail));
            //mail.CC.Add(new MailAddress(toMail));
            //mail.Bcc.Add("cmg.web@gmail.com");

            client.Send(mail);
        }
    }
}
