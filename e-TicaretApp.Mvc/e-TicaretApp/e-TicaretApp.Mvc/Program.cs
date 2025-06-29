using App.Business.Services;
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

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductCommentService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<FileApiService>();

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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
