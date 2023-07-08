using Eticaret.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Data;

public class EticaretDbContext : DbContext
{
    public EticaretDbContext(DbContextOptions<EticaretDbContext> options) : base(options)
    {
    }

    // 1. Tablo Konfigrasyonu
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    // 2. Veritabanı Konfigrasyonu
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // SQL Server
        string connectionString = "Server=(localdb)\\MSSQLLocalDb; Database=EticaretDb;";
        builder.UseSqlServer(connectionString);

        // Sqlite Bağlantısı
        //string connectionString = "Data Source=c:\\Users\\Cem\\Desktop\\EticaretDb.db;";
        //builder.UseSqlite(connectionString);

        base.OnConfiguring(builder);
    }
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1.Entity sınıfın içinde property üstüne Attribute kullanarak ayarlanabilir.
        // [MaxLength(200)] public string Name { get; set; }

        // 2.FluentAPI ile bir entity modelinin özellikleri ayarlanabilir.
        //modelBuilder.Entity<Category>().Property("Id").UseIdentityColumn();
        //modelBuilder.Entity<Category>().Property("Name").IsRequired().HasMaxLength(100);

        // 3.GroupConfiguration ile ayarlanabilir. CategoryConfigration.cs içerisinde ayarlanır.
        //new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EticaretDbContext).Assembly);


        // Seed: Örnek test verilerinin eklenmesi
        //modelBuilder.Entity<Category>().HasData(new Category() { Id = 1, Name = "Elektronik"});
        DbSeeder.SeedTestData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
