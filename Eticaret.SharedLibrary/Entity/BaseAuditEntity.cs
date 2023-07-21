using System.ComponentModel.DataAnnotations;

namespace Eticaret.SharedLibrary.Entity;

public abstract class BaseAuditEntity : BaseEntity
{
	[Required(ErrorMessage = "{0} alanı gerekli")]
	[Display(Name = "Oluşturulma Tarihi", Prompt = "Tarih giriniz")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	[Display(Name = "Güncelleme Tarihi", Prompt = "Tarih giriniz")]
	public DateTime? UpdateAt { get; set; }

	[Display(Name = "Silinme Tarihi", Prompt = "Tarih giriniz")]
	public DateTime? DeletedAt { get; set; }
}
