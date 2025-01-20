using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Models;

namespace BiblioPlomb.Data
{
    public class BiblioPlombDB : DbContext
    {
        public BiblioPlombDB(DbContextOptions<BiblioPlombDB> options)
            : base(options)
        {
        }

        public DbSet<BiblioPlomb.Models.Emprunt> Emprunt { get; set; } = default!;
        public DbSet<BiblioPlomb.Models.Genre> Genre { get; set; } = default!;
        public DbSet<BiblioPlomb.Models.Livre> Livre { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            //modelBuilder.Entity<Genre>()
            //    .HasOne(genre => genre.Livres)
            //    .WithMany(livre => livre.Genres)
            //    .HasForeignKey(genre => genre.LivreId);
        }
        public DbSet<BiblioPlomb.Models.AuteurLivre> AuteurLivre { get; set; } = default!;
    }
}
