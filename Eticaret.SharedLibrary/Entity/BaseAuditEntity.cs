using System.ComponentModel.DataAnnotations;

namespace Eticaret.SharedLibrary.Entity;

public abstract class BaseAuditEntity : BaseEntity
{
    [Required(ErrorMessage = "{0} alanı gerekli")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdateAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
