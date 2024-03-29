﻿using Eticaret.Data;
using Eticaret.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Web.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly EticaretDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _confuguration;

        public ProductsController(EticaretDbContext context, IWebHostEnvironment env, IConfiguration confuguration)
        {
            _context = context;
            _env = env;
            _confuguration = confuguration;
        }

        public async Task<IActionResult> ProductImages(int productId)
        {
            var productImages = await _context.ProductImages
                .Include(m => m.Product)
                .Where(m => m.ProductId == productId)
                .ToListAsync();

            return View(productImages);
        }

        public async Task<IActionResult> ProductImageDelete(int id, int productId)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(m => m.Id == id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ProductImages", new { productId });
        }

        public async Task<IActionResult> SaveProductImage(IFormFile productImage, int productId)
        {
            if (productImage == null)
            {
                TempData["MessageError"] = "<p>Lütfen bir dosya seçiniz.";
                return RedirectToAction("ProductImages", new { productId });
            }

            var msg = "";

            if (productImage.Length > 5 * 1024 * 1024) // byte olduğu için MB için iki kez çarptık.
            {
                msg += "<p>Lütfen 5MB dan küçük boyutta bir resim yükleyiniz.";
            }

            var fileName = productImage.FileName; // turıouroıtu.jpg
            var fileExtension = Path.GetExtension(fileName);
            if (fileExtension != ".jpg" && fileExtension != ".gif" && productImage.ContentType != "image/jpeg")
                msg += "<p>Dosya türü JPG veya GIF olmalıdır.</p>";

            if (string.IsNullOrEmpty(msg))
            {
                var uploadPath = _env.WebRootPath + _confuguration["App:UploadPath"]; // wwwroot/uploads
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                // HH: saat, mm: Dakika, ss: Saniye, fff: Milisaniye
                var randomFileName = "ProductImage_" + productId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //randomFileName = Guid.NewGuid().ToString("N"); // N: Tireler olmadan, D: Tireler ile

                var uploadFilePath = Path.Combine(uploadPath, randomFileName + fileExtension); // wwwroot/uploads/ProductImage_1_202306251251134.jpg

                // Dosya yükleme
                using var stream = new FileStream(uploadFilePath, FileMode.Create);
                await productImage.CopyToAsync(stream);
                stream.Close();

                // Veritabanına kaydet
                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = productId,
                    ImagePath = randomFileName + fileExtension
                });
                await _context.SaveChangesAsync();

                TempData["MessageSuccess"] = "Dosya yüklendi.";
            }
            else
            {
                TempData["MessageError"] = msg;
            }

            return RedirectToAction("ProductImages", new { productId });
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(string query, string orderColumn, string orderDirection, int page = 1)
        {
            var products = _context.Products
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .Select(e => e); // SELECT * FROM Products

            // Filtreleme
            if (query != null)
            {
                // SELECT * FROM Products WHERE Title LIKE '%Aranan%' OR Description LIKE '%Aranan%'
                products = products.Where(e => e.Title.Contains(query) || e.Description.Contains(query));
            }

            // Sıralama
            // SELECT * FROM Products
            // WHERE Title LIKE '%Aranan%' OR Description LIKE '%Aranan%'
            // ORDER BY Title, Price DESC
            // products = products.OrderBy(e => e.Title).ThenByDescending(e=>e.Price);
            if (orderColumn == "title" && orderDirection == "az")
            {
                products = products.OrderBy(e => e.Title);
            }
            else if (orderColumn == "title" && orderDirection == "za")
            {
                products = products.OrderByDescending(e => e.Title);
            }
            else if (orderColumn == "price" && orderDirection == "az")
            {
                products = products.OrderBy(e => e.Price);
            }
            else if (orderColumn == "price" && orderDirection == "za")
            {
                products = products.OrderByDescending(e => e.Price);
            }

            // Sayfalandırma
            var totalItems = await products.CountAsync(); // Veritabanından toplam kayıt sayısını alır           
            products = products.Skip((page - 1) * 5).Take(5);
            ViewBag.TotalPage = Math.Ceiling(totalItems / 5m);

            // Burada veritabanından verileri çekiyoruz
            var productList = await products.ToListAsync();

            return productList != null ?
                View(productList) :
                Problem("Entity set 'EticaretDbContext.Products'  is null.");
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            }).ToListAsync();

            ViewBag.Brands = await _context.Brands.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            }).ToListAsync();

            return View("Form");
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,StockAmount,CreatedAt,IsActive,CategoryId,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Form", product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            }).ToListAsync();

            ViewBag.Brands = await _context.Brands.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            }).ToListAsync();


            return View("Form", product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,StockAmount,CreatedAt,IsActive,CategoryId,BrandId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Form", product);
        }

        // POST: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EticaretDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
