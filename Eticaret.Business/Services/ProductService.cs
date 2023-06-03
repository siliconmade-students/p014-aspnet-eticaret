using Eticaret.Data;
using Eticaret.Data.Entity;

namespace Eticaret.Business.Services
{
    public class ProductService
    {
        private readonly EticaretDbContext _context;

        public ProductService(EticaretDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _context.Products.Where(e => e.CategoryId == categoryId).ToList();
        }
    }
}
