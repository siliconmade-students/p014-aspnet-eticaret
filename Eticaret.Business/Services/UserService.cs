using Eticaret.Data;
using Eticaret.Data.Entity;
using Eticaret.SharedLibrary.Email;

namespace Eticaret.Business.Services
{
    public class UserService
    {
        private readonly EticaretDbContext _context;
        private readonly EmailService _emailService;

        public UserService(EticaretDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public User? GetUserByLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(e =>
                e.EmailAddress == email &&
                e.Password == password);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.EmailAddress == email);
        }

        public int CreateUserAndGetId(string email, string password)
        {
            var newUser = new User()
            {
                EmailAddress = email,
                Password = password,
                Roles = "Customer",
                IsActive = false
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.Id;
        }

        public bool ActivateUser(User user)
        {
            user.ActivationCode = null;
            user.IsActive = true;
            return _context.SaveChanges() > 0;
        }

        public void SendActivationEmail(int userId, string email)
        {
            // Activation Email
            var mailLink = "https://localhost:7181/Auth/Activate?emailAddress=" + email + "&userId=" + userId;
            var logoUrl = "https://localhost:7181/img/logo.png";

            _emailService.Send(email, "Siliconmade - Kayıt İşlemi", @$"
                <style>body {{ font-family: Arial; font-size: 16px; }}</style>
                <img src=""{logoUrl}"" alt=""Logo"">
                <h1>Siliconmade</h1>
                <p>Üyelik kaydınız alınmıştır.</p>
                <p>Aktivasyon için aşağıdaki bağlantıya tıklayınız.
                <p><a href='{mailLink}'>Aktive Et</a>
            ");
        }
    }
}
