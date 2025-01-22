using BiblioPlomb.Data;
using BiblioPlomb.Services;
using BiblioPlomb.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQLite
builder.Services.AddDbContext<BiblioPlombDB>(options =>
    options.UseSqlite("Data Source=BiblioPlomb.db"));

// Add services to the container.
builder.Services.AddControllers(); // Important pour les API
builder.Services.AddControllersWithViews();

// Register Role Service and Repository
builder.Services.AddScoped<IServiceRole, ServiceRole>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Register User Service and Repository
builder.Services.AddScoped<IServiceUtilisateur, ServiceUtilisateur>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

// Register User Service and Repository
builder.Services.AddScoped<IServiceUtilisateur, ServiceUtilisateur>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

// Ajout des services Livre et Genre
builder.Services.AddScoped<ServiceLivre>();
builder.Services.AddScoped<ServiceGenre>();

// Swagger Configuration - PARTIE 1
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BiblioPlomb API",
        Version = "v1",
        Description = "API Bibliothèque BiblioPlomb"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger Configuration - PARTIE 2
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BiblioPlomb API V1");
        c.RoutePrefix = "Swagger";
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Important : ajoutez cette ligne pour les routes API
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();