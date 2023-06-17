using System.Net;
using System.Net.Mail;
using Eticaret.SharedLibrary.Email.Interfaces;

namespace Eticaret.SharedLibrary.Email
{
    public class GmailEmailService : IEmailService
    {
        public void Send(string toMail, string subject, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("cmg.web@gmail.com", "123"),
                EnableSsl = true
            };

            var mail = new MailMessage()
            {
                From = new MailAddress("cmg.web@gmail.com", "Siliconmade Academy"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress(toMail));

            client.Send(mail);
        }
    }
}
