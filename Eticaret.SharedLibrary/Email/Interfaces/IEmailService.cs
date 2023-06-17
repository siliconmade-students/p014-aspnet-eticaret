namespace Eticaret.SharedLibrary.Email.Interfaces
{
    public interface IEmailService
    {
        void Send(string toMail, string subject, string body);
    }
}
