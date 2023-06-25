using Eticaret.SharedLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Data.Entity;

public class Brand : BaseEntity
{
    [Required]
    [Unicode, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation Properties
    //[ValidateNever]
    public ICollection<Product>? Products { get; set; }
}
