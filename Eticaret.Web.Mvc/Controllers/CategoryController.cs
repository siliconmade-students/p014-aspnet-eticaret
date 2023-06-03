using Eticaret.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductService _productService;

        public CategoryController(ProductService productService)
        {
            _productService = productService;
        }

        // /category/index/5
        public IActionResult Index(int id)
        {
            var products = _productService.GetProductsByCategoryId(id);

            return View(products);
        }
    }
}
