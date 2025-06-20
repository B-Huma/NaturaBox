using AdminMvc.Mapping;
using App.Business.Services;
using App.Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddHttpClient("data-api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7196/api/");
});
builder.Services.AddHttpClient("api-file", client =>
{
    client.BaseAddress = new Uri("https://localhost:7119/api/");
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductCommentService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    //await dbContext.Database.EnsureDeletedAsync();
//    await dbContext.Database.EnsureCreatedAsync();
//}

app.Run();
