using System.Net;
using System.Net.Mail;
using Eticaret.SharedLibrary.Email.Interfaces;

namespace Eticaret.SharedLibrary.Email
{

    public class SmtpEmailService : IEmailService
    {
        public void Send(string toMail, string subject, string body)
        {
            var client = new SmtpClient("smtp.siliconmadeacademy.com", 587)
            {
                Credentials = new NetworkCredential("siliconmade", "123"),
                EnableSsl = true
            };

            var mail = new MailMessage()
            {
                From = new MailAddress("info@siliconmadeacademy.com", "Siliconmade Academy"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress(toMail));

            client.Send(mail);
        }
    }
}
