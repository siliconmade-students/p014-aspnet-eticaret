using AutoMapper;
using Eticaret.Business.Dtos;
using Eticaret.Data;
using Eticaret.Data.Entity;
using Eticaret.SharedLibrary.Email.Interfaces;

namespace Eticaret.Business.Services
{
    public interface IUserService
    {
        UserDto? GetUserByLogin(string email, string password);
        UserDto? GetUserByEmail(string email);
        int CreateUserAndGetId(string email, string hashedPassword, string salt);
        bool ActivateUser(int userId);
        void SendActivationEmail(int userId, string email);
        List<UserAddressDto>? GetUserAddresses(int userId);
    }

    public class UserService : IUserService
    {
        private readonly EticaretDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(EticaretDbContext context, IEmailService emailService, IMapper mapper)
        {
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
        }

        public UserDto? GetUserByLogin(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(e =>
                e.EmailAddress == email &&
                e.Password == password);

            return _mapper.Map<UserDto>(user);
        }

        public UserDto? GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.EmailAddress == email);

            return _mapper.Map<UserDto>(user);
        }

        public int CreateUserAndGetId(string email, string hashedPassword, string salt)
        {
            var newUser = new User()
            {
                EmailAddress = email,
                Password = hashedPassword,
                Salt = salt,
                Roles = "Customer",
                IsActive = false
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.Id;
        }

        public bool ActivateUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.ActivationCode = null;
                user.IsActive = true;
            }

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

        public List<UserAddressDto>? GetUserAddresses(int userId)
        {
            var addresses = _context.UserAddresses.Where(e => e.UserId == userId).ToList();

            return _mapper.Map<List<UserAddressDto>>(addresses);
        }
    }
}
