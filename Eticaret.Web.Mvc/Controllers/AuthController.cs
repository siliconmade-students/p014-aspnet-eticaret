using Eticaret.Business.Services;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eticaret.Web.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByLogin(model.EmailAddress, model.Password);

                if (!user.IsActive)
                {
                    ModelState.AddModelError("EmailAddress", "User is not active!");
                    return View(model);
                }

                if (user != null)
                {
                    // Kimlik Bilgileri
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name ?? ""),
                        new Claim(ClaimTypes.GivenName, user.Name ?? ""),
                        new Claim(ClaimTypes.Surname, user.Surname ?? ""),
                        new Claim(ClaimTypes.Email, model.EmailAddress)
                    };

                    if (!string.IsNullOrEmpty(user.Roles))
                    {
                        string[] roles = user.Roles.Split(','); // Admin,Student
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }

                    // Kimlik
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Cüzdan
                    var principal = new ClaimsPrincipal(identity);

                    var props = new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(15),
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        props
                    );

                    return Redirect(returnUrl != "" ? returnUrl : "/");
                }
                else
                {
                    ViewBag.Message = "Username or Password is wrong!";
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        public IActionResult AccessDenied() => View();

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDb = _userService.GetUserByEmail(model.EmailAddress);
                if (userDb != null)
                {
                    ModelState.AddModelError("EmailAddress", "Bu eposta kayıtlı!");
                    return View(model);
                }

                var userId = _userService.CreateUserAndGetId(model.EmailAddress, model.Password);

                _userService.SendActivationEmail(userId, model.EmailAddress);

                return RedirectToAction("RegisterSuccess");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult RegisterSuccess() => View();

        public IActionResult Activate(string emailAddress, string userId)
        {
            var userDb = _userService.GetUserByEmail(emailAddress);
            if (userDb != null)
            {
                _userService.ActivateUser(userDb);

                return RedirectToAction("ActivationSuccess");
            }

            return Redirect("/");
        }

        public IActionResult ActivationSuccess() => View();

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
