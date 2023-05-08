using Eticaret.Web.Mvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EticaretDbContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Veritaban� servisine eri�im sa�lar.
    var context = scope.ServiceProvider.GetRequiredService<EticaretDbContext>();

    // Veritaban�n� sil
    //context.Database.EnsureDeleted();

    // Veritaban�n� olu�turur
    context.Database.EnsureCreated();
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