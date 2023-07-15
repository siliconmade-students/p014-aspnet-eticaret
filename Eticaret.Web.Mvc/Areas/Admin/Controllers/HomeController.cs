using Eticaret.Data;
using Eticaret.Web.Mvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Web.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly EticaretDbContext _context;

        public HomeController(EticaretDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rapor = _context.Products
                .Include(o => o.Category)
                .GroupBy(o => new { o.Category.Name })
                //.Where(cGrp => cGrp.Count() >= 2)
                .Select(g => new RaporKategoriViewModel { Key = g.Key.Name, Count = g.Count() })
                .ToList();

            return View(rapor);
        }
    }
}
