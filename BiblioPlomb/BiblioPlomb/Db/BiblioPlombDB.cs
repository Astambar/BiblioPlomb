using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Models;
using Biblioplomb.Models;

namespace BiblioPlomb.Db
{
    public class BiblioPlombDB : DbContext
    {
        public BiblioPlombDB(DbContextOptions<BiblioPlombDB> options)
            : base(options) { }

        // Définir des DbSets pour les entités
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AuteurLivre> AuteurLivres { get; set; }
        //public DbSet<LivreGenre> LivreGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer les clés composites pour les entités de relation
            modelBuilder.Entity<AuteurLivre>()
                .HasKey(auteurlivre => new { auteurlivre.AuteurId, auteurlivre.LivreId });

            modelBuilder.Entity<AuteurLivre>()
                .HasOne(auteurlivre => auteurlivre.Auteur)
                .WithMany(a => a.AuteurLivres)
                .HasForeignKey(auteurlivre => auteurlivre.AuteurId);

            modelBuilder.Entity<AuteurLivre>()
                .HasOne(auteurlivre => auteurlivre.Livre)
                .WithMany(livre => livre.AuteurLivres)
                .HasForeignKey(auteurlivre => auteurlivre.LivreId);

            modelBuilder.Entity<Livre>()
                .HasOne(livre => livre.Genre)
                .WithMany(genre => genre.Livres)
                .HasForeignKey(livre => livre.GenreId);

            //modelBuilder.Entity<LivreGenre>()
            //    .HasKey(livregenre => new { livregenre.LivreId, livregenre.GenreId });

            //modelBuilder.Entity<LivreGenre>()
            //    .HasOne(livregenre => livregenre.Livre)
            //    .WithMany(livre => livre.LivreGenres)
            //    .HasForeignKey(livregenre => livregenre.LivreId);

            //modelBuilder.Entity<LivreGenre>()
            //    .HasOne(livregenre => livregenre.Genre)
            //    .WithMany(genre => genre.LivreGenres)
            //    .HasForeignKey(livregenre => livregenre.GenreId);
        }
    }
}
