using Eticaret.Business.Dtos;
using Eticaret.Data;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Business.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAll(int page = 1);
        ProductDto? GetById(int id);
        List<ProductDto> GetProductsByCategoryId(int categoryId);
    }

    public class ProductService : IProductService
    {
        private readonly EticaretDbContext _context;

        public ProductService(EticaretDbContext context)
        {
            _context = context;
        }

        public List<ProductDto> GetAll(int page = 1)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .Skip((page - 1) * 10).Take(10)
                .Select(e => new ProductDto
                {
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .ToList();
        }

        public ProductDto? GetById(int id)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .Where(e => e.Id == id)
                .Select(e => new ProductDto
                {
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .FirstOrDefault();
        }

        public List<ProductDto> GetProductsByCategoryId(int categoryId)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .Where(e => e.CategoryId == categoryId)
                .Select(e => new ProductDto
                {
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .ToList();
        }
    }
}
