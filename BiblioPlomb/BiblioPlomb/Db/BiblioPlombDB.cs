using Microsoft.EntityFrameworkCore;
using Biblioplomb.Models;

namespace BiblioPlomb.Db
{
    public class BiblioPlombDB : DbContext
    {
        public BiblioPlombDB(DbContextOptions<BiblioPlombDB> options)
            : base(options)
        {
        }

        // Assure-toi que tu as des DbSet pour toutes tes entités
        public DbSet<Livre> Livres { get; set; } = default!;
        public DbSet<Auteur> Auteurs { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<AuteurLivre> AuteurLivres { get; set; } = default!;
    }
}
