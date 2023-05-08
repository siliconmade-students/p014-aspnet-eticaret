using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //var allProducts = FakeDatabase.AllProducts;

            //return View(allProducts);
            return View();
        }

        // /product/detail/5
        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}
