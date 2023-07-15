using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Web.Mvc.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly EticaretDbContext _context;

        public UserController(EticaretDbContext context)
        {
            _context = context;
        }

        public IActionResult Orders()
        {
            var orders = _context.Orders
                .Where(e => e.UserId == GetUserId())
                .ToList();

            return View(orders);
        }

        public IActionResult OrderDetail(int id)
        {
            var orderProducts = _context.OrderProducts
                .Include(e => e.Order)
                .Include(e => e.Product)
                .Where(e => e.Order != null && e.Order.UserId == GetUserId() && e.Order.Id == id)
                .ToList();

            return View(orderProducts);
        }

        public IActionResult Addresses()
        {
            return View();
        }
    }
}
