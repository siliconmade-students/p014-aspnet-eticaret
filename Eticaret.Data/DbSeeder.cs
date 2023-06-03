using Eticaret.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Data
{
    public class DbSeeder
    {
        public static void SeedTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new() { Id = 1, Name = "Elektronik"},
                new() { Id = 2, Name = "Kitap"},
                new() { Id = 3, Name = "Giyim"},
            });

            modelBuilder.Entity<Brand>().HasData(new List<Brand>
            {
                new() { Id = 1, Name = "Samsung"},
                new() { Id = 2, Name = "Apple"},
                new() { Id = 3, Name = "Can Yayınları"},
                new() { Id = 4, Name = "Adidas"},
                new() { Id = 5, Name = "Nike"}
            });

            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new() {
                    Id = 1,
                    Username = "admin",
                    Password = "123",
                    EmailAddress = "admin@eticaret.dev",
                    IsActive = true,
                    Name = "Admin",
                    Surname = "Admin",
                    Roles = "Admin"
                },
                new() {
                    Id = 2,
                    Username = "customer",
                    Password = "123",
                    EmailAddress = "customer@eticaret.dev",
                    IsActive = true,
                    Name = "Customer",
                    Surname = "Customer",
                    Roles = "Customer"
                }
            });


            var products = new List<Product>();
            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Title = "Product " + i,
                    Description = "Description " + i,
                    Price = (decimal)Random.Shared.NextDouble() * 100,
                    CreatedAt = DateTime.Now,
                    StockAmount = Random.Shared.Next(10, 100),
                    BrandId = Random.Shared.Next(1, 5),
                    CategoryId = Random.Shared.Next(1, 3),
                    IsActive = true
                });
            }
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
