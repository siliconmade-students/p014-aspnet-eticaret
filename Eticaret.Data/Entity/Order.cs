using Eticaret.SharedLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Data.Entity;

public class Order : BaseEntity
{
    [Display(Name = "User")]
    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    [MaxLength(50)]
    public string OrderNumber { get; set; }

    [Required]
    public int UsersAddressId { get; set; }

    [ForeignKey("UsersAddressId")]
    public UserAddress? UsersAddress { get; set; }

    [Required]
    [Precision(10, 2)]
    public decimal TotalPrice { get; set; }

    [MaxLength(50)]
    public string OrderStatus { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
}
