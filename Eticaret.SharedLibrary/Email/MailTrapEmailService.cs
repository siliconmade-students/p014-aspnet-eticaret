using System.Net;
using System.Net.Mail;
using Eticaret.SharedLibrary.Email.Interfaces;

namespace Eticaret.SharedLibrary.Email
{
    public class MailTrapEmailService : IEmailService
    {
        public void Send(string toMail, string subject, string body)
        {
            // SmtpClient: Sunucu ayarları
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("3fcb198edaf1e4", "1818d6d691d359"),
                EnableSsl = true
            };

            // MailMessage: Mail ayarları
            var mail = new MailMessage()
            {
                From = new MailAddress("siliconmade@example.com", "Siliconmade Academy"),
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
