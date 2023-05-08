using System.ComponentModel.DataAnnotations;

namespace Eticaret.Web.Mvc.Data.Entity;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; }

    [Required]
    [MaxLength(150)]
    public string Email { get; set; }

    [MaxLength(100)]
    public string? ActivationCode { get; set; }

    public bool IsActive { get; set; }
}