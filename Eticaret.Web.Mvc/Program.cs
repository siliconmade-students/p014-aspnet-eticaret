using Eticaret.Business;
using Eticaret.SharedLibrary.Email;
using Eticaret.Web.Mvc.Filters;
using Eticaret.Web.Mvc.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Bir filtrenin tüm isteklerde çalışması için Global Filter olarak tanımlanabilir.
    options.Filters.Add<ExecutionTimeFilter>(int.MinValue);
});

builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "EticaretSession";
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.Cookie.Name = "Siliconmade.Cookie";
        o.LoginPath = "/Auth/Login";
        o.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email:MailTrap"));

var app = builder.Build();

// Code First (1. Ensure, 2.Migration)
// Bu kodlama ile veritabanımız proje çalıştırıldığında oluşturulmuş olur.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        ServiceExtensions.SeedData(scope.ServiceProvider);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseGlobalExceptionMiddleware();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Varsyılan geliştirme ortamında DeveloperExceptionPage aktif olarak çalışır.
    //app.UseDeveloperExceptionPage();
}

#region Exception Handler/Middleware Örneği

//var logger = app.Services.GetRequiredService<ILogger<Program>>();
//app.ConfigureExceptionHandler(logger);

//app.UseGlobalExceptionMiddleware();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

#endregion

#region Middleware Örneği
//app.Use(async (context, next) =>
//{
//    var logDirectory = Directory.GetCurrentDirectory() + "\\Logs";

//    if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

//    using var sw = File.AppendText(Path.Combine(logDirectory, "Logs.txt"));
//    var ip = context.Connection.RemoteIpAddress;
//    sw.WriteLine(ip + ";" + DateTime.Now);

//    await next.Invoke();
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 2nd delegate.");
//});

//app.UseMiddleware<IpLoggingMiddleware>();
//app.UseIPLogging();
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();