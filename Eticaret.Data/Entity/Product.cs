using Eticaret.SharedLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Data.Entity;

public class Product : BaseAuditEntity
{
    [Required(ErrorMessage = "{0} alanı gerekli")]
    [Display(Name = "Ürün Adı", Prompt = "Ürün adı giriniz")]
    [Unicode, MaxLength(200, ErrorMessage = "{0} alanına en fazla 200 karakter girebilirsiniz")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Ürün Açıklaması", Prompt = "Ürün açıklaması giriniz")]
    [Unicode, MaxLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "{0} alanı gerekli")]
    [Display(Name = "Fiyat", Prompt = "Ürün fiyatını giriniz")]
    //[Range(1, 1000, ErrorMessage = "Fiyat bilgisi 1-1000 arası olmalıdır")]
    [Precision(12, 3)]
    public decimal Price { get; set; }

    [Display(Name = "Stok Adedi", Prompt = "Stok adedi giriniz")]
    public int? StockAmount { get; set; }

    public bool IsActive { get; set; }

    [Display(Name = "Kategori")]
    public int? CategoryId { get; set; }

    [Display(Name = "Marka")]
    public int? BrandId { get; set; }
    public int ViewCount { get; set; }

    // Navigation Properties
    //[ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    public Brand? Brand { get; set; }
    public List<ProductImage> ProductImages { get; set; }
}
