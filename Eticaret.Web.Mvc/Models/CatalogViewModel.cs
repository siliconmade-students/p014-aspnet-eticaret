using Eticaret.Business.Dtos;
using Eticaret.Data.Entity;

namespace Eticaret.Web.Mvc.Models;

public class CatalogViewModel
{
    public CategoryDto Category { get; set; }
    public List<ProductDto> Products { get; set; }
}