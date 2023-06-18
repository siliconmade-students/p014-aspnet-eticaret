using Eticaret.Business.Dtos;
using Eticaret.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Kayıt Okuma
        // GET: api/products
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            var products = _productService.GetAll();

            return products;
        }

        [HttpGet("GetAllProducts")]
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _productService.GetAll();

            return products;
        }

        // Tek Kayıt Okuma
        // GET api/products/5
        [HttpGet("{id}")]
        public ProductDto? Get(int id)
        {
            var product = _productService.GetById(id);

            return product;
        }

        // Kayıt Ekleme
        // POST api/products
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return "1 row saved!";
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return "1 row updated!";
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
