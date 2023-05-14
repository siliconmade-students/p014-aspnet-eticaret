using Eticaret.Web.Mvc.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EticaretDbContext>(o =>
{
    // appSettings.json içerisindeki Default baðlantý metnini almayý saðlar.
    string connectionString = builder.Configuration.GetConnectionString("Default");
    o.UseSqlServer(connectionString);
});


var app = builder.Build();

// Code First (1. Ensure, 2.Migration)
// Bu kodlama ile veritabanýmýz proje çalýþtýrýldýðýnda oluþturulmuþ olur.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        // Veritabaný servisine eriþim saðlar.
        var context = scope.ServiceProvider.GetRequiredService<EticaretDbContext>();

        // Veritabanýný sil
        //context.Database.EnsureDeleted();

        // Veritabanýný oluþturur
        context.Database.EnsureCreated();

        // Migrationlarý çalýþtýr
        //context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();