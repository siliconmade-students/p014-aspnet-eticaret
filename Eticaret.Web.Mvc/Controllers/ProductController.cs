using Eticaret.Business.Dtos;
using Eticaret.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[SampleActionFilter]
        //[ExecutionTimeFilter]
        public IActionResult Search(int? categoryId, string? query)
        {
            var products = _productService.GetProductsBySearch(new SearchDto
            {
                CategoryId = categoryId,
                Query = query
            });

            return View(products);
        }

        // /product/detail/5
        public IActionResult Detail(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
