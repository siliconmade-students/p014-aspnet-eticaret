using Eticaret.SharedLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Data.Entity;

public class OrderProduct : BaseEntity
{
    public int? OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    [Precision(10, 2)]
    public decimal Price { get; set; }
}