using Eticaret.Web.Mvc.Data;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var allProducts = FakeDatabase.AllProducts;

            var cartProduct1 = new CartProduct { Id = 1, Product = allProducts[0], Quantity = 5, Price = 5.289m };
            var cartProduct2 = new CartProduct { Id = 2, Product = allProducts[1], Quantity = 2, Price = 9.229m };
            var cartProduct3 = new CartProduct { Id = 3, Product = allProducts[0], Quantity = 2, Price = 29.279m };

            var cart = new List<CartProduct> { cartProduct1, cartProduct2, cartProduct3 };

            return View(cart);
        }
    }
}
