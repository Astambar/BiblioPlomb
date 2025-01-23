using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Models;



namespace BiblioPlomb.Data
{
    public class BiblioPlombDB : DbContext
    {
        public BiblioPlombDB(DbContextOptions<BiblioPlombDB> options)
            : base(options) { }

        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AuteurLivre> AuteurLivres { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UtilisateurRole> UtilisateurRoles { get; set; }
        public DbSet<EmpruntRelation> EmpruntRelations { get; set; }

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

            // Configuration de la relation many-to-many entre Utilisateur et Role
            modelBuilder.Entity<UtilisateurRole>()
                .HasKey(ur => new { ur.UtilisateurId, ur.RoleId });

            modelBuilder.Entity<UtilisateurRole>()
                .HasOne(ur => ur.Utilisateur)
                .WithMany(u => u.UtilisateurRoles)
                .HasForeignKey(ur => ur.UtilisateurId);

            modelBuilder.Entity<UtilisateurRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UtilisateurRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configuration de la relation One-to-Many entre Emprunt et Livre et entre Emprunt et Utilisateur
            modelBuilder.Entity<EmpruntRelation>()
                .HasKey(er => new { er.LivreId, er.EmpruntId, er.UtilisateurId });

            modelBuilder.Entity<EmpruntRelation>()
                .HasOne(e => e.Emprunts)
                .WithMany(er => er.EmpruntUtilisateurs)
                .HasForeignKey(e => e.EmpruntId);

            modelBuilder.Entity<EmpruntRelation>()
                .HasOne(l => l.Livres)
                .WithMany(er => er.EmpruntLivres)
                .HasForeignKey(l => l.LivreId);
        }
    }
}
