using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel lvm)
        {
            return View(lvm);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
