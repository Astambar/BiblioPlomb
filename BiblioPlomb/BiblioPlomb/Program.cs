using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BiblioPlomb.Db;
using BiblioPlomb.Services;

var builder = WebApplication.CreateBuilder(args);

// base de données pour utiliser SQLite
builder.Services.AddDbContext<BiblioPlombDB>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BiblioPlombContext")));

// les services pour les contrôleurs et les vues
builder.Services.AddControllersWithViews();

// Ajout des services
builder.Services.AddScoped<LivreService>();
builder.Services.AddScoped<GenreService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Bibliotheque/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Pour les fichiers statiques (CSS, JS, images, etc.)
app.UseRouting();
app.UseAuthorization();

//routes pour les contrôleurs
app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "livre", 
    pattern: "Create/{action=Index}/{id?}", 
    defaults: new { controller = "Livres" });

//app.MapControllerRoute(
//    name: "genre",
//    pattern: "Create/{action=Index}/{id?}",
//    defaults: new { controller = "Roles" });



app.Run();
