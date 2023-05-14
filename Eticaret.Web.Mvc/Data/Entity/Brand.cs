using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Web.Mvc.Data.Entity;

public class Brand
{
    public int Id { get; set; }

    [Required]
    [Unicode, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<Product> Products { get; set; }
}
