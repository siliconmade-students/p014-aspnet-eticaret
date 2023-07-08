using Eticaret.SharedLibrary.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Data.Entity;

public class UserAddress : BaseEntity
{
    [Display(Name = "User")]
    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Address { get; set; }

    [Required]
    [MaxLength(50)]
    public string City { get; set; }

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
}