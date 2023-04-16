using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
