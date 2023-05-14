using Bogus;
using Eticaret.Web.Mvc.Data.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Web.Mvc.Data
{
    public class DbSeeder
    {
        public static void SeedTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new() { Id = 1, Name = "Elektronik" },
                new() { Id = 2, Name = "Kitap" },
                new() { Id = 3, Name = "Giyim" },
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
                    Email = "admin@eticaret.dev",
                    IsActive = true
                }
            });


            //var products = new List<Product>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    products.Add(new Product
            //    {
            //        Id = i,
            //        Title = "Product " + i,
            //        Description = "Description " + i,
            //        Price = (decimal)Random.Shared.NextDouble() * 100,
            //        CreatedAt = DateTime.Now,
            //        StockAmount = Random.Shared.Next(10, 100),
            //        BrandId = Random.Shared.Next(1, 5),
            //        CategoryId = Random.Shared.Next(1, 3),
            //        IsActive = true
            //    });
            //}

            // BOGUS paketi ile gerçekçi örnek veri kullanımı
            int i = 1;
            var productsTr = new Bogus.DataSets.Commerce(locale: "tr");
            var productFaker = new Faker<Product>()
                .RuleFor(o => o.Id, f => i++)
                .RuleFor(o => o.Title, f => productsTr.ProductName())
                .RuleFor(o => o.Description, f => productsTr.ProductDescription())
                .RuleFor(o => o.Price, f => f.Random.Decimal(10, 100))
                .RuleFor(o => o.StockAmount, f => f.Random.Int(10, 100))
                .RuleFor(o => o.BrandId, f => f.Random.Int(1, 5))
                .RuleFor(o => o.CategoryId, f => f.Random.Int(1, 3))
                .RuleFor(o => o.CreatedAt, new DateTime(2023, 05, 14, 9, 41, 25))
                .RuleFor(o => o.IsActive, true)
                ;
            var products = productFaker.Generate(100);
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
