using Eticaret.Business.Services;
using Eticaret.Data;
using Eticaret.SharedLibrary.Email;
using Eticaret.SharedLibrary.Email.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eticaret.Business
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EticaretDbContext>(o =>
            {
                // appSettings.json içerisindeki Default bağlantı metnini almayı sağlar.
                string connectionString = configuration.GetConnectionString("Default");
                o.UseSqlServer(connectionString);
            });

            if (configuration["App:MailServer"] == "Smtp")
            {
                services.AddSingleton<IEmailService, SmtpEmailService>();
            }
            else if (configuration["App:MailServer"] == "Gmail")
            {
                services.AddSingleton<IEmailService, GmailEmailService>();
            }
            else if (configuration["App:MailServer"] == "MailTrap")
            {
                services.AddSingleton<IEmailService, MailTrapEmailService>();
            }
            else
            {
                services.AddSingleton<IEmailService, MailTrapEmailService>();
            }

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }

        public static void SeedData(IServiceProvider serviceProvider)
        {
            // Veritabanı servisine erişim sağlar.
            var context = serviceProvider.GetRequiredService<EticaretDbContext>();

            // Veritabanını sil
            //context.Database.EnsureDeleted();

            // Veritabanını oluşturur
            context.Database.EnsureCreated();

            // Migrationları çalıştır
            //context.Database.Migrate();

            // Verileri Seed yaparken doğrudan veritabanına kayıt ekleme işlemi de kullanılabilir
            //context.Categories.Add(new Category { Name = "Technology" });
            //context.Products.Add(new Product { Title = "Product 1", Description = "Post 1", CategoryId = 1 });
            //context.SaveChanges();
        }
    }
}
