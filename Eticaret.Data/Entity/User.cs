﻿using Eticaret.SharedLibrary.Entity;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Data.Entity;

public class User : BaseEntity
{
    [MaxLength(50)]
    public string? Username { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public string Salt { get; set; }

    [Required]
    [MaxLength(150)]
    public string EmailAddress { get; set; }

    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Surname { get; set; }

    [MaxLength(50)]
    public string? FullAddress { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Roles { get; set; }

    [MaxLength(100)]
    public string? ActivationCode { get; set; }

    public bool IsActive { get; set; }
}