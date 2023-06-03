using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Data.Entity;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Unicode, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<Product> Products { get; set; }
}
