﻿// <auto-generated />
using System;
using BiblioPlomb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioPlomb.Migrations
{
    [DbContext(typeof(BiblioPlombDB))]
    partial class BiblioPlombDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("BiblioPlomb.Models.Auteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Auteurs");
                });

            modelBuilder.Entity("BiblioPlomb.Models.AuteurLivre", b =>
                {
                    b.Property<int>("AuteurId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LivreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuteurId", "LivreId");

                    b.HasIndex("LivreId");

                    b.ToTable("AuteurLivres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Emprunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateRetour")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Emprunts");
                });

            modelBuilder.Entity("BiblioPlomb.Models.EmpruntRelation", b =>
                {
                    b.Property<int>("LivreId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmpruntId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EmpruntId1")
                        .HasColumnType("INTEGER");

                    b.HasKey("LivreId", "EmpruntId", "UtilisateurId");

                    b.HasIndex("EmpruntId");

                    b.HasIndex("EmpruntId1");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("EmpruntRelations");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Livre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("AuteurId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Dispo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Etat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ISBN")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Livres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("BiblioPlomb.Models.UtilisateurRole", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UtilisateurId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UtilisateurRoles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.AuteurLivre", b =>
                {
                    b.HasOne("BiblioPlomb.Models.Auteur", "Auteur")
                        .WithMany("AuteurLivres")
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioPlomb.Models.Livre", "Livre")
                        .WithMany("AuteurLivres")
                        .HasForeignKey("LivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auteur");

                    b.Navigation("Livre");
                });

            modelBuilder.Entity("BiblioPlomb.Models.EmpruntRelation", b =>
                {
                    b.HasOne("BiblioPlomb.Models.Emprunt", "Emprunts")
                        .WithMany("EmpruntUtilisateurs")
                        .HasForeignKey("EmpruntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioPlomb.Models.Emprunt", null)
                        .WithMany("EmpruntLivres")
                        .HasForeignKey("EmpruntId1");

                    b.HasOne("BiblioPlomb.Models.Livre", "Livres")
                        .WithMany("EmpruntLivres")
                        .HasForeignKey("LivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioPlomb.Models.Utilisateur", "Utilisateurs")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emprunts");

                    b.Navigation("Livres");

                    b.Navigation("Utilisateurs");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Livre", b =>
                {
                    b.HasOne("BiblioPlomb.Models.Genre", "Genre")
                        .WithMany("Livres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("BiblioPlomb.Models.UtilisateurRole", b =>
                {
                    b.HasOne("BiblioPlomb.Models.Role", "Role")
                        .WithMany("UtilisateurRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioPlomb.Models.Utilisateur", "Utilisateur")
                        .WithMany("UtilisateurRoles")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Auteur", b =>
                {
                    b.Navigation("AuteurLivres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Emprunt", b =>
                {
                    b.Navigation("EmpruntLivres");

                    b.Navigation("EmpruntUtilisateurs");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Genre", b =>
                {
                    b.Navigation("Livres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Livre", b =>
                {
                    b.Navigation("AuteurLivres");

                    b.Navigation("EmpruntLivres");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Role", b =>
                {
                    b.Navigation("UtilisateurRoles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Utilisateur", b =>
                {
                    b.Navigation("UtilisateurRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
