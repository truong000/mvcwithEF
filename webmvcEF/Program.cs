using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using webmvcEF.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddNToastNotifyNoty(new NotyOptions
    {
        ProgressBar = true,
        Timeout = 5000
    });

//Add Session
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p =>
    {
        p.LoginPath = "/AccountCustomer/LoginAccount";
        p.LogoutPath = "/AccountCustomer/LogoutAccount";
        p.AccessDeniedPath = "/";
    });


builder.Services.AddDbContext<OnlineShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

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

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductsWithAjax}/{action=Index}/{id?}");

app.Run();
