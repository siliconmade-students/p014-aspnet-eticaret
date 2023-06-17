using Eticaret.SharedLibrary.Entity;

namespace Eticaret.Data.Entity;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string ImagePath { get; set; }
}