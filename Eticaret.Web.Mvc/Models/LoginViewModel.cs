namespace Eticaret.Web.Mvc.Models
{
    public class LoginViewModel
    {
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}