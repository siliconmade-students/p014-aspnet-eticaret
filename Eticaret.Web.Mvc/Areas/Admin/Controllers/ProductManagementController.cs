using Eticaret.Web.Mvc.Models;
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
                new () { Text = "Sofa", Value = "Sofa" },
                new () { Text = "Chair", Value = "Chair" },
                new () { Text = "Table", Value = "Table" },
                new () { Text = "Unit", Value = "Unit" },
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Hata kontrolü

            if (product.Title.Contains("Ücretsiz"))
            {
                // Özel durumları kontrol ederek özel hata mesajları üretebiliriz.
                // "Title" gibi bir başlık vererek hata mesajını başlığın altında gösterebiliriz.

                //ModelState.AddModelError("Title", "Ürün adında Ücretsiz ifadesi yer alamaz.");

                // Eğer Title değerini vermeden "" şeklinde kullanırsak hata mesajını sadece Validation Summary alanında görebiliriz. Form elemanı altında göremeyiz.
                ModelState.AddModelError("", "Ürün adında Ücretsiz ifadesi yer alamaz.");
            }

            // Model verileri geçerliyse işlem yapmayı sağlar
            if (ModelState.IsValid)
            {
                // veritabanına kaydet
                ViewBag.Message = "Kayıt başarılı";
                return View();
            }
            else
            {
                ViewBag.Error = "Verileriniz hatalı";

                ViewBag.Categories = new List<SelectListItem>
                {
                    new () { Text = "Sofa", Value = "Sofa" },
                    new () { Text = "Chair", Value = "Chair" },
                    new () { Text = "Table", Value = "Table" },
                    new () { Text = "Unit", Value = "Unit" },
                };

                return View(product);
            }
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
