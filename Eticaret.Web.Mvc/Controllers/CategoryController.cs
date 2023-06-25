using Eticaret.Business.Services;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CategoryController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // /category/index/5
        public IActionResult Index(int id)
        {
            var category = _categoryService.GetById(id);
            var products = _productService.GetProductsByCategoryId(id);

            var vm = new CatalogViewModel
            {
                Category = category,
                Products = products
            };

            return View(vm);
        }
    }
}
