using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Models;

namespace BiblioPlomb.Data
{
    public class BiblioPlombContext : DbContext
    {
        public BiblioPlombContext(DbContextOptions<BiblioPlombContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UtilisateurRole> UtilisateurRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
