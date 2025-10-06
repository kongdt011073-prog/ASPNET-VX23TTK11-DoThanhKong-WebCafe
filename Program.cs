using Microsoft.EntityFrameworkCore;
using SCoffee.Data;
using SCoffee.Models.Interfaces;
using SCoffee.Models.Services;
using Microsoft.AspNetCore.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add
builder.Services.AddDbContext<SCoffeeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SC"),
        sql => sql.EnableRetryOnFailure() 
    )
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SCoffeeDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Add
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(ShoppingCartRepository.GetCart);
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Add
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SCoffeeDbContext>();
    db.Database.Migrate();
    await RoleUser.Createdefaultvalue(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
