using System.ComponentModel.DataAnnotations;

namespace Eticaret.Web.Mvc.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        // Name: Label alanında görmek istediğimiz başlığı ayarlar.
        // Prompt: Placeholder değerini ayarlar.
        // ErrorMessage: Hata mesajlarını özelleştirme. {0} formun Display Name değerini almayı sağlar.
        [Display(Name = "Ürün Adı", Prompt = "Ürün adı giriniz")]
        [MaxLength(200, ErrorMessage = "{0} alanına en fazla 200 karakter girebilirsiniz")]
        [Required(ErrorMessage = "{0} alanı gerekli")]
        public string Title { get; set; }

        [Display(Name = "Ürün Açıklaması", Prompt = "Ürün açıklaması giriniz")]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Fiyat", Prompt = "Ürün fiyatını giriniz")]
        [Required(ErrorMessage = "{0} alanı gerekli")]
        [Range(1, 1000, ErrorMessage = "Fiyat bilgisi 1-1000 arası olmalıdır")]
        public decimal Price { get; set; }

        [Display(Name = "Kategori", Prompt = "Kategori seçiniz")]
        [Required(ErrorMessage = "{0} alanı gerekli")]
        public string Category { get; set; }
    }
}
