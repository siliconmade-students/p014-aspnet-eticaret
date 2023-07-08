using Eticaret.Business.Services;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Eticaret.Web.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cartSessionList = GetCartSessionList();

            return View(cartSessionList);
        }

        private List<CartSessionModel>? GetCartSessionList()
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

        public IActionResult AddToCart(int pid)
        {
            var product = _productService.GetById(pid);
            if (product == null) return RedirectToAction("Index");

            var cartSessionList = GetCartSessionList();

            var activeCartRow = cartSessionList.FirstOrDefault(e => e.ProductId == pid);
            if (activeCartRow != null)
            {
                activeCartRow.Quantity++;
            }
            else
            {
                var newCartProduct = new CartSessionModel
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    Quantity = 1,
                    Price = product.Price
                };
                cartSessionList.Add(newCartProduct);
            }

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartSessionList));

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int pid)
        {
            var product = _productService.GetById(pid);
            if (product == null) return RedirectToAction("Index");

            var cartSessionList = GetCartSessionList();

            var activeCartRow = cartSessionList.FirstOrDefault(e => e.ProductId == pid);
            if (activeCartRow != null)
            {
                activeCartRow.Quantity--;

                if (activeCartRow.Quantity <= 0)
                {
                    cartSessionList.Remove(activeCartRow);
                }
            }

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartSessionList));

            return RedirectToAction("Index");
        }
    }
}
