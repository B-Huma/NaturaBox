using App.Business.Abstract;
using App.Business.Concrete;
using e_TicaretApp.Mvc;
using e_TicaretApp.Mvc.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("access-token")
    .AddCookie("access-token", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddHttpClient("data-api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7196/api/");
}).AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddHttpClient("api-file", client =>
{
    client.BaseAddress = new Uri("https://localhost:7119/api/");
});

builder.Services.AddTransient<AuthenticationHandler>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductCommentService, ProductCommentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFileApiService, FileApiService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
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

// Uploads klasörü için static file serving
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
