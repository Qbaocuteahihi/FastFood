using FastFood.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ
ConfigureServices(builder.Services);

var app = builder.Build();

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Thêm middleware session
app.UseAuthorization();

// Cấu hình route cho controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Phương thức cấu hình dịch vụ
void ConfigureServices(IServiceCollection services)
{
    // Cấu hình DbContext với chuỗi kết nối từ appsettings.json
    services.AddDbContext<FastFoodContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("FastFoodDatabase")));

    services.AddControllersWithViews();
    services.AddScoped<CartController>();

    // Cấu hình session
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true; // Cần thiết cho GDPRx   
    });
}