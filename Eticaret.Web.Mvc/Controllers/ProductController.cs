using Eticaret.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int categoryId)
        {
            var products = _productService.GetProductsByCategoryId(categoryId);

            return View(products);
        }

        // /product/detail/5
        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}
