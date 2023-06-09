﻿using Eticaret.SharedLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Data.Entity;

public class Category : BaseEntity
{
    [Required]
    [Unicode, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<Product> Products { get; set; }
}
