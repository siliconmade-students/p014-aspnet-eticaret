using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Eticaret.Web.Mvc.Controllers
{
    public class BaseController : Controller
    {
        // /base/getcartsessionlist
        [NonAction]
        public List<CartSessionModel>? GetCartSessionList()
        {
            // Boş bir sepet listesi oluştur
            var cartSessionList = new List<CartSessionModel>();

            // Daha önce session da Cart isminde sepet bilgisi varsa listeyi bununla oluştur.
            var cartSession = HttpContext.Session.GetString("Cart"); // string
            if (cartSession != null)
            {
                cartSessionList = JsonSerializer.Deserialize<List<CartSessionModel>>(cartSession);
            }

            return cartSessionList;
        }


        public int? GetUserId()
        {
            var nameIdentifier = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(nameIdentifier)) return null;

            return Convert.ToInt32(nameIdentifier);
        }
    }
}
