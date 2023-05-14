using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Web.Mvc.Data.Entity;

public class Product
{
    public int Id { get; set; }

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
    //[Column("ProductPrice", TypeName = "decimal(12, 3)")]
    public decimal Price { get; set; }

    public int? StockAmount { get; set; }

    [Required(ErrorMessage = "{0} alanı gerekli")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }


    // Navigation Properties
    //[ForeignKey("CategoryId")]
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
}
