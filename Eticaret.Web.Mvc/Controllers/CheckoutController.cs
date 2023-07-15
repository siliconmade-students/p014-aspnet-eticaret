using Eticaret.Business.Services;
using Eticaret.Data;
using Eticaret.Data.Entity;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eticaret.Web.Mvc.Controllers
{
    [Authorize]
    public class CheckoutController : BaseController
    {
        private readonly IUserService _userService;
        private readonly EticaretDbContext _context;

        public CheckoutController(IUserService userService, EticaretDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cartSessionList = GetCartSessionList();

            var vm = new CheckoutModel
            {
                Cart = cartSessionList,
                UserAddresses = _userService.GetUserAddresses(GetUserId() ?? 0)
                    .Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString()
                    })
                    .ToList()
            };

            return View(vm);
        }

        public IActionResult Pay(CheckoutModel model)
        {
            // Kredi Kartı Kontrolü
            // Ödeme sistemine bir istek gönderdikten sonra gelen istekteki başarılı/hatalı duruma göre kontrol sağlanabilir.
            if (model.CreditCard.Number == "123")
            {

                TempData["ErrorMessage"] = "Kredi Kartı geçerli değil!";
                return RedirectToAction("PaymentError");
            }

            var cartSessionList = GetCartSessionList();
            var totalPrice = cartSessionList.Sum(item => item.Quantity * item.Price);

            // Veritabanı
            var order = new Order
            {
                UserId = GetUserId(),
                OrderNumber = "CM-" + GetUserId() + "-" + Random.Shared.Next(10000, 99999),
                TotalPrice = totalPrice,
                UsersAddressId = model.SelectedUserAddress,
                OrderStatus = "Siparişiniz alındı",
                CreatedAt = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartSessionList)
            {
                _context.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Amount = item.Quantity,
                    Price = item.Price
                });
            }
            _context.SaveChanges();

            return RedirectToAction("PaymentSuccess");
        }


        public IActionResult PaymentSuccess() => View();
        public IActionResult PaymentError() => View();
    }
}
