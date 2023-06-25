using Eticaret.Business.Dtos;
using Eticaret.Data;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Business.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAll(int page = 1);
        ProductDto? GetById(int id);
        List<ProductDto> GetLastProducts(int take = 4);
        List<ProductDto> GetPopularProducts(int take = 4);
        List<ProductDto> GetProductsByCategoryId(int categoryId);
        List<ProductDto> GetProductsBySearch(SearchDto searchDto);
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
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .ToList();
        }

        public ProductDto? GetById(int id)
        {
            var product = _context.Products
                .Include(e => e.ProductImages)
                .Where(e => e.Id == id)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .FirstOrDefault();

            return product;
        }

        public List<ProductDto> GetLastProducts(int take = 4)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .Take(take)
                .ToList();
        }

        public List<ProductDto> GetPopularProducts(int take = 4)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .OrderByDescending(e => e.ViewCount)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .Take(take)
                .ToList();
        }

        public List<ProductDto> GetProductsByCategoryId(int categoryId)
        {
            return _context.Products
                .Include(e => e.ProductImages)
                .Where(e => e.CategoryId == categoryId)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .ToList();
        }

        public List<ProductDto> GetProductsBySearch(SearchDto searchDto)
        {
            var productsQuery = _context.Products
               .Include(e => e.ProductImages)
               .Select(e => e);

            if (searchDto.CategoryId != null)
                productsQuery = productsQuery.Where(e => e.CategoryId == searchDto.CategoryId);

            if (!string.IsNullOrEmpty(searchDto.Query))
                productsQuery = productsQuery.Where(e => e.Title.Contains(searchDto.Query));



            return productsQuery
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.Title,
                    Price = e.Price,
                    ImagePaths = e.ProductImages.Select(e => e.ImagePath).ToList(),
                })
                .ToList();
        }
    }
}
