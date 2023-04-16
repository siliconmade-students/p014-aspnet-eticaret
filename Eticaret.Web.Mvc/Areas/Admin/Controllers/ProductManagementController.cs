using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eticaret.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new List<SelectListItem>
            {
                new () { Text = "Sofa", Value = "1" },
                new () { Text = "Chair", Value = "2" },
                new () { Text = "Table", Value = "3" },
                new () { Text = "Unit", Value = "4" },
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(int categoryId, string name, decimal price)
        {
            // Hata kontrolü
            // Ürün kayıt işlemleri

            return View();
        }


        public IActionResult Edit(int id)
        {



            ViewBag.Categories = new List<SelectListItem>
            {
                new () { Text = "Sofa", Value = "1" },
                new () { Text = "Chair", Value = "2" },
                new () { Text = "Table", Value = "3" },
                new () { Text = "Unit", Value = "4" },
            };

            return View();
        }


    }
}
