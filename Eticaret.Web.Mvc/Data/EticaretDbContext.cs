using Eticaret.Web.Mvc.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Web.Mvc.Data;

public class EticaretDbContext : DbContext
{
    // 1. Tablo Konfigrasyonu
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    // 2. Veritabanı Konfigrasyonu
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDb; Database=EticaretDb;";
        builder.UseSqlServer(connectionString);
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // FluentAPI ile bir entity modelinin özellikleri ayarlanabilir.
        modelBuilder.Entity<Category>().Property("Id").UseIdentityColumn();
        modelBuilder.Entity<Category>().Property("Name").IsRequired().HasMaxLength(100);

        // Seed: Örnek test verilerinin eklenmesi
        DbSeeder.SeedTestData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
