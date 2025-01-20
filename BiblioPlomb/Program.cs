using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BiblioPlomb.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BiblioPlombDB>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BiblioPlombDB") ?? throw new InvalidOperationException("Connection string 'BiblioPlombDB' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
