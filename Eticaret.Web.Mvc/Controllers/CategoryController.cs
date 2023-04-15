using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        // /category/index/5
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
