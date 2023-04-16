using Eticaret.Web.Mvc.Models;

namespace Eticaret.Web.Mvc.Data
{
    public class FakeDatabase
    {
        public static List<Product> AllProducts = new List<Product>() {
            new Product
            {
                ProductId = 1,
                Title = "Elma",
                Description = "Amasya Elması",
                Price = 5.25m,
                Category = "Gıda"
            },
            new Product
            {
                ProductId = 2,
                Title = "Armut",
                Description = "Ankara Armutu",
                Price = 9.25m,
                Category = "Gıda"
            },
            new Product
            {
                ProductId = 3,
                Title = "Kiraz",
                Description = "Antalya Kirazı",
                Price = 29.25m,
                Category = "Gıda"
            },
            new Product
            {
                ProductId = 4,
                Title = "iPhone 14",
                Description = "iPhone'nun son modeli",
                Price = 32399m,
                Category = "Elektronik"
            },
            new Product
            {
                ProductId = 5,
                Title = "Samsung S23",
                Description = "Samsung'un son modeli",
                Price = 25499m,
                Category = "Elektronik"
            }
        };
    }
}
